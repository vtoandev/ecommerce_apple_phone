using System;
using AutoMapper;
using ecommerce_apple_phone.DTO;
using ecommerce_apple_phone.EF;

namespace ecommerce_apple_phone.Helper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //Fees
            CreateMap<Fee, FeeDTO>();
            CreateMap<FeeDTO, Fee>();
            //Category
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            //Info
            CreateMap<InfoDTO, Info>();
            CreateMap<Info, InfoDTO>();
            //MethodPay
            CreateMap<MethodPay, MethodPayDTO>();
            CreateMap<MethodPayDTO, MethodPay>();
            //Product
            CreateMap<Product, ProductDTO>()
                .ForMember(des => des.Name, act => act.MapFrom(src => src.ProductDetail.Name))
                .ForMember(des => des.ROM, act => act.MapFrom(src => src.ProductDetail.ROM))
                .ForMember(des => des.Id, act => act.MapFrom(src => src.ProductDetailId + "-" + src.Id))
                .ForMember(des => des.CategoryId, act => act.MapFrom(src => src.ProductDetail.CategoryId));
            CreateMap<ProductDTO, Product>()
                .ForMember(des => des.Id, act => act.MapFrom(src => getPropID(src.Id, 1)))
                .ForMember(des => des.ProductDetailId, act => act.MapFrom(src => getPropID(src.Id, 0)));

            CreateMap<ProductDetail, ProductDetailDTO>();
            CreateMap<ProductDetailDTO, ProductDetail>();
            //
            CreateMap<ProductDetailDTO, ProductDetail>();
            CreateMap<ProductDetailDTO, ProductDetail>();
            //Post
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            //Feedback
            CreateMap<Feedback, FeedbackDTO>();
            CreateMap<FeedbackDTO, Feedback>();
            //Import
            CreateMap<ImportDetail, ImportDetailDTO>();
            CreateMap<ImportDetailDTO, ImportDetail>();
            CreateMap<ImportProduct, ImportProductDTO>();
            CreateMap<ImportProductDTO, ImportProduct>();
            //Order
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<OrderDetailDTO, OrderDetail>();
            //Prom
            CreateMap<PromotionDTO, Promotion>();
            CreateMap<Promotion, PromotionDTO>();
            //
            CreateMap<PromBillDTO, PromBill>();
            CreateMap<PromBill, PromBillDTO>();
            //
            CreateMap<PromPoint, PromPointDTO>();
            CreateMap<PromPointDTO, PromPoint>();
            //
            CreateMap<PromProductDTO, PromProduct>();
            CreateMap<PromProduct, PromProductDTO>();
        }

        private int getPropID(string idstring, int idx)
        {
            return Int32.Parse(idstring.Split("-")[idx]);
        }
    }
}


