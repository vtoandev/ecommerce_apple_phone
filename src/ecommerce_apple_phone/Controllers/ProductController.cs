using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_apple_phone.DTO;
using ecommerce_apple_phone.Helper;
using ecommerce_apple_phone.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace ecommerce_apple_phone.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class ProductController : ControllerBase {

        public IProductModel _productModel;
        public ICacheHelper _cache;
        public IPromotionModel _promotionModel;
        public ProductController (IProductModel productModel, ICacheHelper cache, IPromotionModel promotionModel) {
            _productModel = productModel;
            _cache = cache;
            _promotionModel = promotionModel;
        }

        #region Client
        [HttpGet]
        public ActionResult<List<ProductDTO>> GetListProduct () {
            var re = GetListProducts ();
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return re;
        }

        [HttpGet ("get-ids/{stringId}")]
        public ActionResult<List<ProductDTO>> FindByIds (string stringId) {
            if (stringId == null || stringId.Length <= 0) return BadRequest ();
            string[] arIds = stringId.Split (",");
            //
            var re = GetListProducts ();
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return _productModel.FindByIds (re, arIds);
        }

        [HttpGet ("best-seller")]
        public ActionResult<List<ProductDTO>> FindBestSeller () {
            //Get in cache
            var re = _cache.Get<List<ProductDTO>> (CacheKey.SELLER_PRODUCT);
            if (re != null || re?.Count > 0) return re;
            //Get in db
            var products = GetListProducts ();
            if (products == null) return Problem (statusCode: 500, detail: "Data not exist");
            re = _productModel.FindBestSeller (products);
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            _cache.Set (re, CacheKey.SELLER_PRODUCT);
            return re;
        }

        [HttpGet ("cate/{cateId}")]
        public ActionResult<List<ProductDTO>> FindByCate (int cateId) {
            if (cateId <= 0) return BadRequest ();
            //
            var re = GetListProducts ();
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return _productModel.FindByCate (re, cateId);
        }

        [HttpGet ("promotion")]
        public ActionResult<List<ProductDTO>> FindPromotion () { //
            //Get in cache
            var re = _cache.Get<List<ProductDTO>> (CacheKey.DISCOUNT_PRODUCT);
            if (re != null || re?.Count > 0) return re;
            //Get in db
            var products = GetListProducts ();
            if (products == null) return Problem (statusCode: 500, detail: "Data not exist");
            re = _productModel.FindPromotion (products);
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            _cache.Set (re, CacheKey.DISCOUNT_PRODUCT);
            return re;
        }

        [HttpGet ("search/{query}")]
        public ActionResult<List<ProductDTO>> FindProduct (string query) {
            if (query == null || query == "") return BadRequest ();
            //
            var re = GetListProducts ();
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return _productModel.FindByString (re, query);
        }

        #endregion
        // =============== detail product ===============
        #region Product
        [HttpGet ("{id}")]
        public ActionResult<ProductDetailDTO> GetDetail (int id) {
            if (id <= 0) return BadRequest ();
            var re = _productModel.GetDetailDTO (id);
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return re;
        }

        [HttpPost]
        public ActionResult AddProduct (int cateId, ProductDetailDTO productDetailDTO) {
            var modified = new PropModified<ProductDetailDTO> (productDetailDTO);
            if (!modified.isChanged || cateId <= 0) return BadRequest ();
            var re = _productModel.AddDTOs (cateId, productDetailDTO);
            if (re == null) return Problem (statusCode: 500, detail: "Can't add data");
            return Ok ();
        }

        [HttpPut ("{id}")]
        public ActionResult UpdateProduct (int id, ProductDetailDTO productDetailDTO) {
            var modified = new PropModified<ProductDetailDTO> (productDetailDTO);
            if (!modified.isChanged || id <= 0) return BadRequest ();
            var re = _productModel.UpdateDTO (id, productDetailDTO);
            if (!re) return Problem (statusCode: 500, detail: "Can't update data");
            return Ok ();
        }

        [HttpPut ("status/{id}")]
        public ActionResult UpdateStatus (int id, bool status) {
            if (id <= 0) return BadRequest ();
            var re = _productModel.UpdateStatusDTO (id, status);
            if (!re) return Problem (statusCode: 500, detail: "Can't update status data");
            return Ok ();
        }

        [HttpDelete ("{id}")]
        public ActionResult RemoveProduct (int id) {
            if (id <= 0) return BadRequest ();
            var re = _productModel.RemoveDTO (id);
            if (!re) return Problem (statusCode: 500, detail: "Can't remove data");
            return Ok ();
        }
        #endregion
        // =============== Sub attibute product ===============
        #region Attribute Product
        [HttpGet ("attr/{id}")]
        public ActionResult<ProductDTO> GetAttr (string id) {
            if (id!=null || id!="") return BadRequest ();
            string[]itemId= id.Split("-");
            var re = _productModel.GetAttrDTO (Int32.Parse(itemId[1]));
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return re;
        }

        [HttpGet ("list-attr/{id}")]
        public ActionResult<List<ProductDTO>> GetListAttr (string id) {
            if (id!=null || id!="") return BadRequest ();
            string[]itemId= id.Split("-");
            var re = _productModel.GetListAttrDTOs (Int32.Parse(itemId[1]));
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return re;
        }

        [HttpPost ("attr/{id}")]
        public ActionResult AddProductAttr (int productId, ProductDTO ProductAttrDTO) {
            var modified = new PropModified<ProductDTO> (ProductAttrDTO);
            if (!modified.isChanged || productId <= 0) return BadRequest ();
            var re = _productModel.AddAttrDTOs (productId, ProductAttrDTO);
            if (re == null) return Problem (statusCode: 500, detail: "Can't add data");
            return Ok ();
        }

        [HttpPut ("attr/{id}")]
        public ActionResult UpdateProductAttr (int id, ProductDTO productAttrDTO) {
            var modified = new PropModified<ProductDTO> (productAttrDTO);
            if (!modified.isChanged || id <= 0) return BadRequest ();
            var re = _productModel.UpdateAttrDTO (id, productAttrDTO);
            if (!re) return Problem (statusCode: 500, detail: "Can't update data");
            return Ok ();
        }

        [HttpPut ("attr/status/{id}")]
        public ActionResult UpdateStatusAttr (int id, bool status) {
            if (id <= 0) return BadRequest ();
            var re = _productModel.UpdateStatusAttrDTO (id, status);
            if (!re) return Problem (statusCode: 500, detail: "Can't update status data");
            return Ok ();
        }

        [HttpDelete ("attr/{id}")]
        public ActionResult RemoveProductAttr (int id) {
            if (id <= 0) return BadRequest ();
            var re = _productModel.RemoveAttrDTO (id);
            if (!re) return Problem (statusCode: 500, detail: "Can't remove data");
            return Ok ();
        }
        #endregion
        // ===============  ===============
        [HttpPost ("image")]
        public ActionResult UpdateImageSEO ([FromServices] IUploadService upload, IFormFile file) {
            if(file==null) return BadRequest();
            if(!upload.UploadFile(file,"products")) return  Problem (statusCode: 500, detail: "Can't upload file");
            return Ok();
        }

        [NonAction]
        private List<ProductDTO> GetListProducts () {
            var re = _cache.Get<List<ProductDTO>> (CacheKey.PRODUCT);
            if (re != null || re?.Count > 0)  return re;
            re = _productModel.GetListDTOs ();
            if (re != null && re?.Count > 0) {
                List<PromProductDTO> proms = _promotionModel.GetListDTOsPromProduct ();
                if (proms != null || proms?.Count > 0)
                    _productModel.AttachDiscount (ref re, proms);
                _cache.Set (re, CacheKey.PRODUCT);
            }
            return re;
        }

    }
}