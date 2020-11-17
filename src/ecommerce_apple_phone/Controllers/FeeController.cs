using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_apple_phone.DTO;
using ecommerce_apple_phone.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace ecommerce_apple_phone.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class FeeController : ControllerBase {

        private IFeeModel _feeModel;

        public FeeController (IFeeModel feeModel) {
            _feeModel = feeModel;
        }

        [HttpGet]
        public ActionResult<List<FeeDTO>> Get () {
            var re = _feeModel.GetListDTOs ();
            if (re == null || re.Count == 0) return Problem (statusCode: 500, detail: "Data not exist");
            return re;
        }

        [HttpGet ("{id}")]
        public ActionResult<FeeDTO> Get (int id) {
            if (id <= 0) return BadRequest (new { message = "ID is invalid" });
            var re = _feeModel.GetDTO (id);
            if (re == null) return Problem (statusCode: 500, detail: "Data not exist");
            return re;
        }

        [HttpPut ("{id}")]
        public IActionResult Update (int id, FeeDTO fee) {
            if (id <= 0 || id != fee.Id) return BadRequest (new { message = "ID is invalid" });
            if (!_feeModel.UpdateDTO (id, fee)) return Problem (statusCode: 500, detail: "Can't update data");
            return Ok ();
        }

        [HttpPost]
        public IActionResult Add (FeeDTO fee) {
            if (fee.Id != 0) return BadRequest (new { message = "Add method is invalid, field 'ID' not require" });
            var re = _feeModel.AddDTO (fee);
            if (re == null) return Problem (statusCode: 500, detail: "Can't add data");
            return Ok ();
        }

        [HttpDelete ("{id}")]
        public IActionResult Remove (int id) {
            if (id <= 0) return NotFound ();
            if (!_feeModel.RemoveDTO (id)) return Problem (statusCode: 500, detail: "Can't remove data");
            return Ok ();
        }

    }
}