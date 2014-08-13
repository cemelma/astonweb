using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using System.Web;

namespace BLL.ProductBL
{
    public class ProductManager
    {
        #region ProductSubGroup
        public static List<ProductSubGroup> GetProductSubGroupList(string language, int groupid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductSubGroup.Where(d => d.Deleted == false && d.ProductGroupId == groupid).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }


        public static List<ProductSubGroup> GetProductSubGroupListForFront(string language, int groupid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductSubGroup.Where(d => d.Deleted == false && d.ProductGroupId == groupid && d.Online == true).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }


        public static bool AddProductSubGroup(ProductSubGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.ProductSubGroup.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupAdded;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static bool EditProductSubGroup(ProductSubGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubGroup editrecord = db.ProductSubGroup.Where(d => d.ProductSubGroupId == record.ProductSubGroupId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.GroupName = record.GroupName;
                        editrecord.ProductGroupId = record.ProductGroupId;
                        editrecord.PageSlug = record.PageSlug;
                        if (!string.IsNullOrEmpty(record.GroupImage))
                            editrecord.GroupImage = record.GroupImage;

                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        //logkeeper.Message = LogMessages.ProductGroupAdded;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.GroupName;
                        //logkeeper.AddInfoLog(logger);


                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static bool UpdateSubGroupStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductSubGroup.SingleOrDefault(d => d.ProductSubGroupId == id);
                try
                {

                    if (list != null)
                    {
                        list.Online = list.Online == true ? false : true;
                        db.SaveChanges();

                    }
                    return list.Online;

                }
                catch (Exception)
                {
                    return list.Online;
                }
            }
        }

        public static bool DeleteSubGroup(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ProductSubGroup.FirstOrDefault(d => d.ProductSubGroupId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupDeleted;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ProductSubGroup GetProductSubGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubGroup record = db.ProductSubGroup.Where(d => d.ProductSubGroupId == nid).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool EditProductSubGroup(int id, string name, string pageslug)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubGroup record = db.ProductSubGroup.Where(d => d.ProductSubGroupId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {

                        record.GroupName = name;
                        record.PageSlug = pageslug;
                        record.TimeUpdated = DateTime.Now;
                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        //logkeeper.Message = LogMessages.ProductGroupEdited;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.GroupName;
                        //logkeeper.AddInfoLog(logger);

                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public static bool SortSubRecords(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        ProductSubGroup sortingrecord = db.ProductSubGroup.SingleOrDefault(d => d.ProductSubGroupId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
                        db.SaveChanges();
                        row++;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion

        #region ProductGroup
        public static List<ProductGroup> GetProductGroupList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductGroup.Where(d => d.Deleted == false && d.Language == language).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static ProductGroup GetProductGroupItem(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductGroup.FirstOrDefault(d => d.Deleted == false && d.ProductGroupId == id);
                return list;
            }
        }

        public static List<ProductGroup> GetProductGroupListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductGroup.Where(d => d.Deleted == false && d.Language == language && d.Online==true).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<ProductDetail> GetProductDetailListForFront(int productId)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductDetail.Where(d => d.ProductId == productId).ToList();
                return list;
            }
        }

        public static bool AddProductGroup(ProductGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.ProductGroup.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupAdded;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public static bool EditProductGroup(ProductGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductGroup editrecord = db.ProductGroup.Where(d => d.ProductGroupId == record.ProductGroupId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.GroupName = record.GroupName;
                        editrecord.PageSlug = record.PageSlug;
                        if (!string.IsNullOrEmpty(record.GroupImage))
                            editrecord.GroupImage = record.GroupImage;

                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        //logkeeper.Message = LogMessages.ProductGroupAdded;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.GroupName;
                        //logkeeper.AddInfoLog(logger);


                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }
        
        public static bool UpdateGroupStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductGroup.SingleOrDefault(d => d.ProductGroupId == id);
                try
                {

                    if (list != null)
                    {
                        list.Online = list.Online == true ? false : true;
                        db.SaveChanges();

                    }
                    return list.Online;

                }
                catch (Exception)
                {
                    return list.Online;
                }
            }
        }


        public static bool DeleteGroup(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ProductGroup.FirstOrDefault(d => d.ProductGroupId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupDeleted;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ProductGroup GetProductGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductGroup record = db.ProductGroup.Where(d => d.ProductGroupId == nid).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool EditProductGroup(int id, string name, string pageslug)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductGroup record = db.ProductGroup.Where(d => d.ProductGroupId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {

                        record.GroupName = name;
                        record.PageSlug = pageslug;
                        record.TimeUpdated = DateTime.Now;
                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        //logkeeper.Message = LogMessages.ProductGroupEdited;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.GroupName;
                        //logkeeper.AddInfoLog(logger);

                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public static bool SortRecords(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    string[] itemlist;
                    int listid=0;
                    foreach (var item in idsList)
                    {
                        if (item != "")
                        {
                            itemlist = item.Split('-');
                            listid = Convert.ToInt32(itemlist[0]);
                            ProductGroup sortingrecord = db.ProductGroup.SingleOrDefault(d => d.ProductGroupId == listid);
                            sortingrecord.SortNumber = Convert.ToInt32(itemlist[1]);
                            db.SaveChanges();
                        }
                    }

                    //foreach (string id in idsList)
                    //{
                    //    int mid = Convert.ToInt32(id);
                    //    ProductGroup sortingrecord = db.ProductGroup.SingleOrDefault(d => d.ProductGroupId == mid);
                    //    sortingrecord.SortNumber = Convert.ToInt32(row);
                    //    db.SaveChanges();
                    //    row++;
                    //}
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion ProductGroup

        #region Product

        public static bool SortProducts(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Product sortingrecord = db.Product.SingleOrDefault(d => d.ProductId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
                        db.SaveChanges();
                        row++;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static List<Product> GetProductListAll(string lang)
        {
            using (MainContext db = new MainContext())
            {
                db.Product.Include("ProductGroup").ToList();
                var list = db.Product.Where(d => d.Deleted == false ).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<Product> GetProductListAllForFront(string lang)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.
                            Include("ProductGroup").
                            Include("ProductSubGroup").
                            Include("ProductSubbestGroup").
                            Where(d => d.Deleted == false && 
                                //  d.Language == lang && 
                                  d.Online==true).
                            OrderByDescending(d=>d.TimeCreated).
                            OrderBy(d => d.SortNumber).
                            ToList();
                return list;
            }
        }

        public static List<Product> GetProductList(int gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.Include("ProductGroup").Include("ProductSubGroup").Include("ProductSubbestGroup").Where(d => d.Deleted == false && d.ProductGroupId == gid).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<Product> GetProductList(int [] gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.Where(x => gid.Contains(x.ProductGroupId)).ToList();
                return list;
            }
        }
        

        public static List<Product> GetProductListForFront(int gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.
                    Include("ProductGroup").
                    Include("ProductSubGroup").
                    Where(d => d.Deleted == false && 
                                d.Online == true && 
                                d.ProductGroupId == gid).
                    OrderByDescending(d => d.TimeCreated).
                    OrderBy(d => d.SortNumber).
                    ToList();
                return list;
            }
        }

        public static List<Product> GetProductListBySubGroupForFront(int sgid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.
                            Include("ProductGroup").
                            Include("ProductSubGroup").
                            Where(d => d.Deleted == false &&
                                        d.Online == true ).
                                        //1d.ProductSubGroupId == sgid).
                            OrderByDescending(d => d.TimeCreated).
                            OrderBy(d => d.SortNumber).
                            ToList();
                return list;
            }
        }
        public static List<Product> GetProductListBySubGroup(int sgid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.Include("ProductGroup").Include("ProductSubGroup").Where(d => d.Deleted == false ).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static bool AddProduct(Product record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.Product.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                    //logkeeper.Message = LogMessages.ProductAdded;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.Name;
                    //logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static bool EditProduct(Product data)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == data.ProductId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Name = data.Name;
                        record.Content = data.Content;
                        record.PageSlug = data.PageSlug;
                        record.IsShownMain = data.IsShownMain;
                     
                        //record.ProductGroupId = data.ProductGroupId;
                        if (!string.IsNullOrEmpty(data.Image1) && data.Image1 != "/Content/images/front/noimage.jpeg")
                        {
                            string filePath = HttpContext.Current.Server.MapPath(record.Image1.Replace("/userfiles/productthumb/", "/userfiles/productbig/"));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            string filePatht = HttpContext.Current.Server.MapPath(record.Image1);
                            if (System.IO.File.Exists(filePatht))
                            {
                                System.IO.File.Delete(filePatht);
                            }

                            record.Image1 = data.Image1;
                        }

                        if (!string.IsNullOrEmpty(data.Image2) && data.Image2 != "/Content/images/front/noimage.jpeg")
                       {
                           string filePath = HttpContext.Current.Server.MapPath(record.Image2.Replace("/userfiles/productthumb/", "/userfiles/productbig/"));
                           if (System.IO.File.Exists(filePath))
                           {
                               System.IO.File.Delete(filePath);
                           }

                           string filePatht = HttpContext.Current.Server.MapPath(record.Image2);
                           if (System.IO.File.Exists(filePatht))
                           {
                               System.IO.File.Delete(filePatht);
                           }

                           record.Image2 = data.Image2;
                       }
                       
                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                        //logkeeper.Message = LogMessages.ProductEdited;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.Name;
                        //logkeeper.AddInfoLog(logger);
                        return true;
                    }
                    else
                        return false;



                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static List<Product> GetProductListFront(int topid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.Where(d => d.Deleted == false && d.Online == true && d.TopProductGroupId == topid).OrderByDescending(d => d.TimeCreated).
                    OrderBy(d => d.SortNumber).
                    ToList();
                return list;
            }
        }

        public static List<Product> GetProductTopListFront(int[] topid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.Where(d => d.Deleted == false && d.Online == true && topid.Contains(d.TopProductGroupId)).OrderByDescending(d => d.TimeCreated).
                    OrderBy(d => d.SortNumber).
                    ToList();
                return list;
            }
        }

        public static List<Product> GetProductTopListFrontMainPage(int[] topid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.Where(d => d.Deleted == false && d.Online == true && d.IsShownMain == true && topid.Contains(d.TopProductGroupId)).OrderByDescending(d => d.TimeCreated).
                    OrderBy(d => d.SortNumber).
                    ToList();

                //Ürünlerin hepsini görünür ya anasayfada
                //var li = db.Product.ToList();
                //foreach (var item in li)
                //{
                //    item.IsShownMain = true;
                //}
                //db.SaveChanges();
                return list;
            }
        }

        #endregion Product


        public static object UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Product.SingleOrDefault(d => d.ProductId == id);
                try
                {

                    if (list != null)
                    {
                        list.Online = list.Online == true ? false : true;
                        db.SaveChanges();

                    }
                    return list.Online;

                }
                catch (Exception)
                {
                    return list.Online;
                }
            }
        }

        public static object DeleteProduct(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Product.FirstOrDefault(d => d.ProductId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                    //logkeeper.Message = LogMessages.ProductDeleted;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.Name;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Product GetProductById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Product record = db.Product.Include("ProductGroup").Where(d => d.ProductId == nid && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Product> GetProductByIds(Dictionary<string, string>[] ids)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    List<Product> list = new List<Product>();
                    
                    foreach (var element in ids)
                    {
                        foreach (var entry in element)
                        {
                            Product p = ProductManager.GetProductById(Convert.ToInt32(entry.Value));
                            list.Add(p);
                        }
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }



        public static bool RemoveTechnic(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                       // record.filetechnical = null;
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                                      
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static object RemoveTraining(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                    //    record.filetraining = null;
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static object RemoveExperimental(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        //record.filexperiment = null;
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static object RemoveVideo(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                  //      record.filevideo = null;
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static ProductGroup GetGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductGroup record = db.ProductGroup.Where(d => d.ProductGroupId == nid && d.Deleted==false).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }


        }

        public static ProductSubGroup GetSubGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubGroup record = db.ProductSubGroup.Where(d => d.ProductSubGroupId == nid && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool GetLoginControl(string mail, string pass)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    User record = db.User.FirstOrDefault(d => d.Email == mail && d.Password == pass && d.Deleted == false && d.Online == true);
                    if (record != null)
                        return true;
                    else return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool AddUser(User record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeUpdated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    db.User.Add(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public static bool GetMailControl(string mail)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    User record = db.User.FirstOrDefault(d => d.Email == mail && d.Deleted == false);
                    if (record == null)
                        return true;
                    else return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static ProductGroup GetTopGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductGroup record = db.ProductGroup.Where(d => d.ProductGroupId == nid && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }


        }

    }
}
