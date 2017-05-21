
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
namespace MVCVSMobile.Models
{
    public class Sanpham
    {
        VSMobileDataContext db = new VSMobileDataContext();


        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public List<Product> ListSize(int productId)
        {
            return db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToList();
        }
        public IEnumerable<Product> ListAllByTag(string tag, int page, int pageSize)
        {
            var query = (from a in db.Products
                         join b in db.ProductTags
                         on a.ID equals b.ProductID
                         where b.TagID == tag
                         select new
                         {
                             ID = a.ID,
                             Name = a.Name,
                             MetaTitle = a.Metatitle,
                             Image = a.Image,
                             MoreImage = a.MoreImage,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice,
                             Quantity = a.Quantity,
                             Description = a.Description,
                             Detail = a.Detail,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             UpdatedDate = a.UpdatedDate,
                             UpdatedBy = a.UpdatedBy,
                             ViewCount = a.ViewCount,
                             Status = a.Status

                         }).AsEnumerable().Select(x => new Product()
                         {
                             ID = x.ID,
                             Name = x.Name,
                             Metatitle = x.MetaTitle,
                             Image = x.Image,
                             MoreImage = x.MoreImage,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice,
                             Quantity = x.Quantity,
                             Description = x.Description,
                             Detail = x.Detail,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             UpdatedDate = x.UpdatedDate,
                             UpdatedBy = x.UpdatedBy,
                             ViewCount = x.ViewCount,
                             Status = x.Status
                         });
            return query.OrderByDescending(x=>x.Name).ToList();
        }

    }
}