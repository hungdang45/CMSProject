using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMSProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Category
            routes.MapRoute(
                name: "IndexCategories",
                url: "danh-sach-danh-muc",
                defaults: new { controller = "Categories", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "CreateCategories",
           url: "tao-moi-danh-muc",
           defaults: new { controller = "Categories", action = "Create", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "EditCategories",
           url: "chinh-sua-danh-muc",
           defaults: new { controller = "Categories", action = "Edit", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "DetailsCategories",
           url: "chi-tiet-danh-muc",
           defaults: new { controller = "Categories", action = "Details", id = UrlParameter.Optional }
       );
            //product
            routes.MapRoute(
            name: "IndexProducts",
            url: "danh-sach-san-pham",
            defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
           name: "CreateProducts",
           url: "tao-moi-san-pham",
           defaults: new { controller = "Products", action = "Create", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "EditProducts",
           url: "chinh-sua-san-pham",
           defaults: new { controller = "Products", action = "Edit", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "DetailsProducts",
           url: "chi-tiet-san-pham",
           defaults: new { controller = "Products", action = "Details", id = UrlParameter.Optional }
       );
            //blog
            routes.MapRoute(
           name: "IndexBlogs",
           url: "danh-sach-bai-viet",
           defaults: new { controller = "Blogs", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
           name: "CreateBlogs",
           url: "tao-moi-bai-viet",
           defaults: new { controller = "Blogs", action = "Create", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "EditBlogs",
           url: "chinh-sua-bai-viet",
           defaults: new { controller = "Blogs", action = "Edit", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "DetailsBlogs",
           url: "chi-tiet-bai-viet",
           defaults: new { controller = "Blogs", action = "Details", id = UrlParameter.Optional }
       );
            //order
            routes.MapRoute(
            name: "IndexOrders",
            url: "danh-sach-don-hang",
            defaults: new { controller = "Orders", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "CreateOrders",
             url: "tao-moi-bai-viet",
             defaults: new { controller = "Orders", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "EditOrders",
           url: "chinh-sua-bai-viet",
           defaults: new { controller = "Orders", action = "Edit", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "DetailsOrders",
           url: "chi-tiet-bai-viet",
           defaults: new { controller = "Orders", action = "Details", id = UrlParameter.Optional }
       );
            //feedback
            routes.MapRoute(
            name: "IndexFeedbacks",
            url: "danh-sach-feedback",
            defaults: new { controller = "Feedbacks", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "CreateFeedbacks",
             url: "tao-moi-feedback",
             defaults: new { controller = "Feedbacks", action = "Create", id = UrlParameter.Optional }
);

            routes.MapRoute(
           name: "EditFeedbacks",
           url: "chinh-sua-feedback",
           defaults: new { controller = "Feedbacks", action = "Edit", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "DetailsFeedbacks",
           url: "chi-tiet-feedback",
           defaults: new { controller = "Feedbacks", action = "Details", id = UrlParameter.Optional }
       );
            //receipts
            routes.MapRoute(
            name: "IndexReceipts",
            url: "danh-sach-bien-lai",
            defaults: new { controller = "Receipts", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "CreateReceipts",
             url: "tao-moi-bien-lai",
             defaults: new { controller = "Receipts", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "EditReceipts",
           url: "chinh-sua-bien-lai",
           defaults: new { controller = "Receipts", action = "Edit", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "DetailsReceipts",
           url: "chi-tiet-bien-lai",
           defaults: new { controller = "Receipts", action = "Details", id = UrlParameter.Optional }
       );
            //supplier
            routes.MapRoute(
            name: "IndexSuppliers",
            url: "danh-sach-nha-phan-phoi",
            defaults: new { controller = "Suppliers", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "CreateSuppliers",
             url: "tao-moi-nha-phan-phoi",
             defaults: new { controller = "Suppliers", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "EditSuppliers",
           url: "chinh-sua-nha-phan-phoi",
           defaults: new { controller = "Suppliers", action = "Edit", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "DetailsSuppliers",
           url: "chi-tiet-nha-phan-phoi",
           defaults: new { controller = "Suppliers", action = "Details", id = UrlParameter.Optional }
       );
            //orderdetail
            routes.MapRoute(
            name: "OrderDetails",
            url: "chi-tiet-don-hang",
            defaults: new { controller = "OrderDetails", action = "Index", id = UrlParameter.Optional }
        );
            //customer
            routes.MapRoute(
            name: "IndexCustomers",
            url: "danh-sach-khach-hang",
            defaults: new { controller = "Customers", action = "Index", id = UrlParameter.Optional }
        );
            //OrderReport
            routes.MapRoute(
            name: "IndexOrderReport",
            url: "bao-cao-don-hang",
            defaults: new { controller = "OrderReports", action = "Index", id = UrlParameter.Optional }
        );
            //original code
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
