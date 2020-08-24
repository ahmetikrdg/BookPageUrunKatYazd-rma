using System.Collections.Generic;
using System.Linq;
using bookpage.data.Abstract;
using bookpage.entity;
using Microsoft.EntityFrameworkCore;

namespace bookpage.data.Concrate.EfCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        public List<Product> GetPopularProducts()
        {
            using(var context=new ShopContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetProductDetails(int id)
        {//left join uygulayacağız.Ürünü alıcam ve ona ait kategoriyide alıcam.
            using(var context=new ShopContext())
            {
                return context.Products
                .Where(i=>i.ProductId==id)//product entitysinin tüm bilgilerini alıyorum bunun yanında productcategoriese geçmek istiyorum onun üzerindende ilgili category bilgisine geçmek istiyorum
                .Include(i=>i.ProductCategories)//producttan productcategoriese gittim 
                .ThenInclude(i=>i.Categories)//ordanda categoriese
                .FirstOrDefault();//kayıt varsa iligli idye uyan product varsa bunu getir ve getirirkende ekstra join işlemleri yapmış oluyorum.şimdi bunları service katmanındada kullanmam lazım          
            }
        } 
    } 
}