using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using System.Security.Cryptography;
using DAL;
using DAL.ViewModel;


namespace DAL.Repository
{
    public class CustomRepository : IDisposable
    {
        private RestaurantEntities context;

       
        public CustomRepository(RestaurantEntities context)
        {
            this.context = context;
        }

        public string GetUserID(string userName)
        {
            string result = context.AspNetUsers.Where(u => u.UserName == userName).Select(s => s.Id).ToString();
            return result;
        }

        public string GetUserName(string userID)
        {
            string result = context.AspNetUsers.Where(u => u.Id == userID).Select(s => s.UserName).FirstOrDefault();
            return result;
        }

        //public IQueryable<VM_Category> GetAllCategory()
        //{
        //    var result = from b in context.Categories
        //                 select new VM_Category
        //                 {
        //                     CategoryOrder = b.CategoryOrder,
        //                     CategoryId = b.CategoryId,
        //                     Description = b.Description,
        //                     Name = b.Name,
        //                     EditDate = b.EditDate
        //                 };

        //    return result;
        //}

        //public VM_Category GetCategoryById(int id)
        //{

        //    var result = context.Categories.Where(s => s.CategoryId == id).Select(
        //        a => new VM_Category
        //        {
        //            CategoryId = a.CategoryId,
        //            Name = a.Name,
        //            CategoryOrder = a.CategoryOrder,
        //            Description = a.Description,
        //            EditDate = a.EditDate
        //        }).FirstOrDefault();

        //    return result;
        //}

        //public IQueryable<VM_SubCategory> GetAllSubCategory()
        //{
        //    var result = from b in context.SubCategories
        //                 join c in context.Categories on b.CategoryId equals c.CategoryId

        //                 select new VM_SubCategory
        //                 {

        //                     SubCategoryId = b.SubCategoryId,
        //                     Name = b.Name,
        //                     Description = b.Description,
        //                     SubCategoryOrder = b.SubCategoryOrder,
        //                     CategoryId = b.CategoryId,
        //                     CategoryName = b.Category.Name,
        //                     EditDate = b.EditDate
        //                 };

        //    return result;
        //}

        //public VM_SubCategory GetSubCategoryById(int id)
        //{
        //    var result = context.SubCategories.Where(s => s.SubCategoryId == id).Select(
        //       a => new VM_SubCategory
        //       {
        //           SubCategoryId = a.SubCategoryId,
        //           Name = a.Name,
        //           SubCategoryOrder = a.SubCategoryOrder,
        //           Description = a.Description,
        //           CategoryId = a.CategoryId,
        //           CategoryName = a.Category.Name,
        //           EditDate = a.EditDate
        //       }).FirstOrDefault();
        //    return result;
        //}

        //public IEnumerable<VM_SubCategory> GetSubCategoryByCalegoryId(int id)
        //{
        //    var result = context.SubCategories.Where(s => s.CategoryId == id).Select(
        //        a => new VM_SubCategory
        //        {
        //            SubCategoryId = a.SubCategoryId,
        //            Name = a.Name,
        //            SubCategoryOrder = a.SubCategoryOrder,
        //            Description = a.Description,
        //            CategoryId = a.CategoryId,
        //            CategoryName = a.Category.Name,
        //            EditDate = a.EditDate
        //        }).ToList();
        //    return result;
        //}


        //public IQueryable<VM_Product> GetAllProducts()
        //{
        //    var result = from b in context.Products
        //                 join c in context.Categories on b.CategoryId equals c.CategoryId
        //                 join d in context.SubCategories on b.SubCategoryId equals d.SubCategoryId
            
            

        //                 select new VM_Product
        //                 {
        //                     ProductId = b.ProductId,
        //                     Name = b.Name,
        //                     English = b.English,
        //                     Banglish = b.Banglish,
        //                     Description = b.Description,
        //                     Unit = b.Unit,
        //                     Category = b.Category.Name,
        //                     SubCategory = b.SubCategory.Name,
        //                     Image = b.Image,
        //                     BuyingPrice = b.BuyingPrice,
        //                     SellingPrice = b.SellingPrice,
        //                     Discount = b.Discount,
        //                 };

           
        //    return result;
        //}


        //public VM_Product GetProductById(int id)
        //{
        //    var result = context.Products.Where(s => s.ProductId == id).Select(
        //       a => new VM_Product
        //       {
        //           ProductId = a.ProductId,
        //           Name = a.Name,
        //           English = a.English,
        //           Banglish = a.Banglish,
        //           Description = a.Description,
        //           CategoryId = a.CategoryId,
        //           SubCategoryId = a.SubCategoryId ?? 0,
        //           Discount = a.Discount,
        //           BuyingPrice = a.BuyingPrice,
        //           SellingPrice = a.SellingPrice,
        //           Unit = a.Unit,
        //           Image = a.Image,
        //       }).FirstOrDefault();
        //    return result;
        //}

        //public VM_Product GetSuggestedProductById(int id)
        //{
        //    var result = context.NewProducts.Where(s => s.NewProductId == id).Select(
        //       a => new VM_Product
        //       {
        //           ProductId = a.NewProductId,
        //           Name = a.Name,
        //           Description = a.Description,
        //           Unit = a.Unit,
        //       }).FirstOrDefault();
        //    return result;
        //}

        //public IQueryable<VM_Product> GetAllProductsBySubCategoryId(int subCategoryId)
        //{
        //    var result = from b in context.Products
        //                 join c in context.Categories on b.CategoryId equals c.CategoryId
        //                 join d in context.SubCategories on b.SubCategoryId equals d.SubCategoryId
        //                 where d.SubCategoryId == subCategoryId

        //                 select new VM_Product
        //                 {
        //                     ProductId = b.ProductId,
        //                     Name = b.Name,
        //                     English = b.English,
        //                     Banglish = b.Banglish,
        //                     Description = b.Description,
        //                     Unit = b.Unit,
        //                     Category = b.Category.Name,
        //                     SubCategory = b.SubCategory.Name,
        //                     Image = b.Image,
        //                     BuyingPrice = b.BuyingPrice,
        //                     SellingPrice = b.SellingPrice,
        //                     Discount = b.Discount,
        //                 };


        //    return result;
        //}

        //public IEnumerable<VM_Order> GetTodaysOrders()
        //{
        //    var result = (from b in context.Orders
        //                 join c in context.Customers on b.CustomerId equals c.CustomerId
        //                 select new VM_Order
        //                 {
        //                     OrderId = b.OrderId,
        //                     CustomerId = b.CustomerId,
        //                     CustomerName = b.Customer.Name,
        //                     OrderDate = b.OrderDate,
        //                     Status = b.Status,
        //                     RequestedShipmentDate = b.RequestedShipmentDate,
        //                     ShippingAddress = c.Address,
        //                     Phone = c.Phone,
        //                     Email = c.Email,
        //                 }).ToList().Where(x => x.OrderDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).Select(
        //                 a=> new VM_Order
        //                 {
        //                     OrderId = a.OrderId,
        //                     CustomerId = a.CustomerId,
        //                     CustomerName = a.CustomerName,
        //                     OrderDate = a.OrderDate,
        //                     Status = a.Status,
        //                     RequestedShipmentDate = a.RequestedShipmentDate,
        //                     ShippingAddress = a.ShippingAddress,
        //                     Phone = a.Phone,
        //                     Email = a.Email,
        //                 });
        //    return result;
        //}


        //public IEnumerable<VM_Order> GetOrdersByShippingDate(DateTime date)
        //{
        //    var result = (from b in context.Orders
        //                  join c in context.Customers on b.CustomerId equals c.CustomerId
        //                  select new VM_Order
        //                  {
        //                      OrderId = b.OrderId,
        //                      CustomerId = b.CustomerId,
        //                      CustomerName = b.Customer.Name,
        //                      OrderDate = b.OrderDate,
        //                      Status = b.Status,
        //                      RequestedShipmentDate = b.RequestedShipmentDate,
        //                      ShippingAddress = c.Address,
        //                      Phone = c.Phone,
        //                      Email = c.Email,
        //                  }).ToList().Where(x => x.RequestedShipmentDate.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Select(
        //                 a => new VM_Order
        //                 {
        //                     OrderId = a.OrderId,
        //                     CustomerId = a.CustomerId,
        //                     CustomerName = a.CustomerName,
        //                     OrderDate = a.OrderDate,
        //                     Status = a.Status,
        //                     RequestedShipmentDate = a.RequestedShipmentDate,
        //                     ShippingAddress = a.ShippingAddress,
        //                     Phone = a.Phone,
        //                     Email = a.Email,
        //                 });
        //    return result;
        //}


        //public IEnumerable<VM_Order> GetOrdersByDate(DateTime date)
        //{
        //    var result = (from b in context.Orders
        //                  join c in context.Customers on b.CustomerId equals c.CustomerId
        //                  select new VM_Order
        //                  {
        //                      OrderId = b.OrderId,
        //                      CustomerId = b.CustomerId,
        //                      CustomerName = b.Customer.Name,
        //                      OrderDate = b.OrderDate,
        //                      Status = b.Status,
        //                      RequestedShipmentDate = b.RequestedShipmentDate,
        //                      ShippingAddress = c.Address,
        //                      Phone = c.Phone,
        //                      Email = c.Email,
        //                  }).ToList().Where(x => x.OrderDate.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Select(
        //                 a => new VM_Order
        //                 {
        //                     OrderId = a.OrderId,
        //                     CustomerId = a.CustomerId,
        //                     CustomerName = a.CustomerName,
        //                     OrderDate = a.OrderDate,
        //                     Status = a.Status,
        //                     RequestedShipmentDate = a.RequestedShipmentDate,
        //                     ShippingAddress = a.ShippingAddress,
        //                     Phone = a.Phone,
        //                     Email = a.Email,
        //                 });
        //    return result;
        //}

        //public IQueryable<Category> GetCategoryWithSubcategory()
        //{
           
        //    var result = context.Categories
        //                .ToList()
        //                .Select(item => 
        //                    new Category
        //                    {
        //                        CategoryId = item.CategoryId,
        //                        Name = item.Name,
        //                        Description = item.Description,
        //                        CategoryOrder = item.CategoryOrder,
        //                        SubCategories = item.SubCategories.Where(a=>a.CategoryId == item.CategoryId).Select(
        //                                                b=>new SubCategory
        //                                                {
        //                                                    SubCategoryId = b.SubCategoryId,
        //                                                    Name = b.Name,
        //                                                    Description = b.Description,
        //                                                    SubCategoryOrder = b.SubCategoryOrder
        //                                                }).OrderBy(x=>x.SubCategoryOrder).ToList()
                                
        //                    }).OrderBy(x=>x.CategoryOrder).AsQueryable();
        //    return result;
        //}


        //public IQueryable<VM_Product> GetSuggestedProducts()
        //{
        //    var result =(from a in context.NewProducts
        //                 join b in context.Customers on a.CustomerId equals b.CustomerId
        //                 join c in context.Orders on a.OrderId equals c.OrderId

        //                 select new VM_Product
        //                 {
        //                     ProductId = a.NewProductId,
        //                     Name = a.Name,
        //                     Description = a.Description,
        //                     Unit = a.Unit,
        //                 });
        //    return result;
        //}


        //public IEnumerable<VM_OrderDetail> OrderDetailsByDate(DateTime date)
        //{
        //    var result = (from a in context.OrderDetails
        //        //join b in context.Orders on a.OrderId equals b.OrderId
        //        //join c in context.Customers on b.CustomerId equals c.CustomerId

        //        select new VM_OrderDetail
        //        {
        //            OrderId = a.OrderId,
        //            OrderDate = a.Order.OrderDate,
        //            CustomerId = a.Order.CustomerId,
        //            CustomerName = a.Order.Customer.Name,
        //            ProductId = a.ProductId,
        //            ProductName = a.Product.Name,
        //            Quantity = a.Quantity,
        //            Unit = a.Product.Unit,


        //        }).ToList().Where(x => x.OrderDate.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Select(
        //            a => new VM_OrderDetail
        //            {
        //                OrderId = a.OrderId,
        //                OrderDate = a.OrderDate,
        //                CustomerId = a.CustomerId,
        //                CustomerName = a.CustomerName,
        //                ProductId = a.ProductId,
        //                ProductName = a.ProductName,
        //                Quantity = a.Quantity,
        //                Unit = a.Unit,
        //            }).ToList();
        //    return result;
        //}

        //public IQueryable<VM_ServiceAreas> GetServiceAreas()
        //{
        //    var result = (from a in context.ServiceAreas

        //                  select new VM_ServiceAreas
        //                  {
        //                     // AreaCode = a.AreaCode,
        //                      ServiceAreaName = a.ServiceAreaName
        //                  });
        //    return result;
        //}


        //public IQueryable<VM_OrderByUploadedBazarList> GetOrderByUploadedBazarLists()
        //{
        //    var result = (from a in context.OrderByUploadedBazarLists where a.Status == 0

        //                  select new VM_OrderByUploadedBazarList
        //                  {
        //                      Id = a.Id,
        //                      CustomerId = a.CustomerId,
        //                      CustomerName = a.Customer.Name,
        //                      Email = a.Customer.Email,
        //                      Phone = a.Customer.Phone,
        //                      Image = a.Image,
        //                      OrderDate = a.OrderDate,
        //                      ShipmentAddress = a.Customer.Address,
        //                      RequestShipmentDate = a.RequestShipmentDate,
        //                      Status = a.Status
        //                  });
        //    return result;
        //}

        //public IQueryable<VM_OrderDetail> GetOrderDetailsByOrderId(int OrderId)
        //{
        //    var result = (from a in context.OrderDetails
        //                  where a.OrderId == OrderId

        //                  select new VM_OrderDetail
        //                  {
        //                        OrderDetailsId = a.OrderDetailsId,
        //                        ProductId = a.ProductId,
        //                        Quantity = a.Quantity,
        //                        OrderId = a.OrderId
        //                  });
        //    return result;
        //}


        //public VM_Customer GetCustomerInfoById(int id)
        //{
        //    var result = (from a in context.Customers
        //                  where a.CustomerId == id

        //                  select new VM_Customer
        //                  {
        //                     CustomerId = a.CustomerId,
        //                     Name = a.Name,
        //                     Address = a.Address,
        //                     Phone = a.Phone,
        //                     Email = a.Email,
        //                     City = a.City
        //                  }).FirstOrDefault();
        //    return result;
        //}

        //public VM_Order GetOrderInfoById(int id)
        //{
        //    var result = (from a in context.Orders
        //                  where a.OrderId == id

        //                  select new VM_Order
        //                  {
        //                      OrderId = a.OrderId,
        //                      CustomerId = a.CustomerId,
        //                      OrderDate = a.OrderDate,
        //                      ShipmentDate = a.ShipmentDate,
        //                      RequestedShipmentDate = a.RequestedShipmentDate,
        //                      Status = a.Status                           
        //                  }).FirstOrDefault();
        //    return result;
        //}



        /// <summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>list</returns>
        /// <CreteadBy>Shohid</CreteadBy>
        /// <CreatedDate>Jan-03-2017</CreatedDate>
        /// </summary>
        //public IEnumerable<VM_OrderDetail> sp_GetOrderDetailsByDate(DateTime toDate)
        //{
        //    SqlParameter OrderDate = new SqlParameter("@OrderDate", toDate);
        //    var result = context.Database.SqlQuery<VM_OrderDetail>("rspOrderWise @OrderDate", OrderDate).ToList();
        //    return result;
        //}

        public IEnumerable<int> sp_GetProductAvailableQuantity(int productId, int storeId)
        {
            SqlParameter StoreId = new SqlParameter("@storeId", storeId);
            SqlParameter ProductId = new SqlParameter("@productId", productId);
            //var result = context.Database.SqlQuery<int>(" @OrderDate", OrderDate).ToList();
            var result = context.Database.SqlQuery<int>("GetProductAvailableQuantity @storeId, @productId", StoreId, ProductId);
            return result;
        }

        public int sp_BackUpDatabase(string path)
        {
            try
            {
                SqlParameter Path = new SqlParameter("@path", path);

                var result = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,"BackUpDatabase @path", Path);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public List<VM_Product> sp_GetMainStoreToProductionHouseStatus(DateTime fromDate, DateTime toDate, int storeId)
        //{
        //    var a = context.sp_mainStoreToProductionHouse(fromDate, toDate, storeId);
        //    var k = a.Select(d => new VM_Product()
        //    {
        //        ProductName = d.ProductName,
        //        Unit = d.Unit,
        //        UnitPrice = Convert.ToDecimal(d.UnitPrice),
        //        StoreName = d.store_name,
        //        Quantity = Convert.ToDecimal(d.Quantity)

        //    }).ToList();

        //    return k;

        //}

        public List<VM_Product> sp_GetMainStoreToProductionHouseStatus(DateTime fromDate, DateTime toDate, int storeId)
        {
            var a = context.sp_mainStoreToProductionHouseFinal(fromDate, toDate, storeId);
            var k = a.Select(d => new VM_Product()
            {
                ProductName = d.ProductName,
                Unit = d.Unit,
                UnitPrice = Convert.ToDecimal(d.UnitPrice),
                StoreName = d.store_name,
                Quantity = Convert.ToDecimal(d.Quantity)

            }).ToList();

            return k;

        }




        //public List<VM_Product> spSearchProductEntryHistoryInSellsPoint(DateTime fromDate, DateTime toDate, int storeId, int shiftId)
        //{
        //    var productList = context.spSearchProductEntryHistoryInSellsPoint(fromDate,toDate,storeId,shiftId)
        //        .Select(a => new VM_Product()
        //        {
        //            ProductName = a.ProductName,
        //            ProductId = (int)a.ProductId,
        //            UnitPrice = (decimal)a.UnitPrice,
        //            Unit = a.Unit,
        //            Quantity = (decimal)a.Quantity,
        //            TotalPrice = (decimal)a.TotalPrice,
        //            ProductTypeName = a.ProductTypeName

        //        })
        //        .ToList();

        //    return productList;

        //}

        public List<VM_Product> spSearchProductEntryHistoryInSellsPoint(DateTime fromDate, DateTime toDate, int storeId, int shiftId)
        {
            var productList = context.spSearchProductEntryHistoryInSellsPointFinal(fromDate, toDate, storeId, shiftId)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    UnitPrice = (decimal)a.UnitPrice,
                    Unit = a.Unit,
                    Quantity = (decimal)a.Quantity,
                    TotalPrice = (decimal)a.TotalPrice,
                    ProductTypeName = a.ProductTypeName

                })
                .ToList();

            return productList;

        }



        public List<VM_Product> sp_ProductEntryHistoryForSpecificDayInSellsPoint(DateTime fromDate, int storeId, int shiftId)
        {
            var productList = context.sp_ProductEntryHistoryForSpecificDayInSellsPoint(fromDate, storeId, shiftId)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    UnitPrice = (decimal)a.UnitPrice,
                    Unit = a.Unit,
                    Quantity = (decimal)a.Quantity,
                    TotalPrice = (decimal)a.TotalPrice,
                    ProductTypeName = a.ProductTypeName

                })
                .ToList();

            return productList;

        }



       
        //public List<VM_Product> sp_ProductEntryHistoryFromSupplierToMainStore(DateTime fromDate, DateTime toDate, int supplierId)
        //{
        //    var productList = context.sp_ProductEntryHistoryFromSupplierToMainStoreForReporting(fromDate, toDate, supplierId)
        //        .Select(a => new VM_Product()
        //        {
        //            ProductId = (int)a.ProductId,
        //            ProductName = a.ProductName,
        //            Quantity = (decimal)a.Quantity,
        //            UnitPrice = (decimal)a.UnitPrice,
        //            TotalPrice = (decimal)a.TotalPrice,
        //            RestaurantId = (int)a.RestaurantId

        //        })
        //        .ToList();

        //    return productList;

        //}


        public List<VM_Product> sp_ProductEntryHistoryFromSupplierToMainStore(DateTime fromDate, DateTime toDate, int supplierId)
        {
            var productList = context.sp_ProductEntryHistoryFromSupplierToMainStoreForReportingFinal(fromDate, toDate, supplierId)
                .Select(a => new VM_Product()
                {
                    ProductId = (int)a.ProductId,
                    ProductName = a.ProductName,
                    Quantity = (decimal)a.Quantity,
                    UnitPrice = (decimal)a.UnitPrice,
                    TotalPrice = (decimal)a.TotalPrice,
                    RestaurantId = (int)a.RestaurantId

                })
                .ToList();

            return productList;

        }


        //public List<DAL.ViewModel.VM_Product> sp_ProductEntryHistoryForaSpecificDateFromSupplierToMainStore(DateTime date)
        //{
        //    var productList = context.sp_ProductEntryHistoryForaSpecificDateFromSupplierToMainStore(date)
        //        .Select(a => new DAL.ViewModel.VM_Product()
        //        {
        //            SupplierId = a.SupplierId,
        //            SupplierName = a.SupplierName,
        //            ProductId = (int)a.ProductId,
        //            ProductName = a.ProductName,
        //            Quantity = Convert.ToDecimal(a.Quantity),
        //            UnitPrice = Convert.ToDecimal(a.UnitPrice),
        //            TotalPrice = Convert.ToDecimal(a.TotalPrice),
        //            RestaurantId = (int)a.RestaurantId

        //        })
        //        .ToList();
        //    return productList;
        //}


        public List<DAL.ViewModel.VM_Product> sp_ProductEntryHistoryForaSpecificDateFromSupplierToMainStore(DateTime date)
        {
            var productList = context.sp_ProductEntryHistoryForaSpecificDateFromSupplierToMainStoreFinal(date)
                .Select(a => new DAL.ViewModel.VM_Product()
                {
                    SupplierId = a.SupplierId,
                    SupplierName = a.SupplierName,
                    ProductId = (int)a.ProductId,
                    ProductName = a.ProductName,
                    Quantity = Convert.ToDecimal(a.Quantity),
                    UnitPrice = Convert.ToDecimal(a.UnitPrice),
                    TotalPrice = Convert.ToDecimal(a.TotalPrice),
                    RestaurantId = (int)a.RestaurantId

                })
                .ToList();
            return productList;
        }


        //public List<VM_Product> sp_ProductSell(DateTime fromDate, DateTime toDate, int storeId)
        //{
        //    var productsellList = context.sp_ProductSell(fromDate, toDate, storeId)
        //        .Select(a => new VM_Product()
        //        {
                   
        //            ProductId = (int)a.ProductId,
        //            ProductName = a.ProductName,
        //            Quantity = (decimal)a.Quantity,
        //            Unit =  a.Unit,
        //            UnitPrice = (decimal)a.UnitPrice,
        //            TotalPrice = (decimal)a.TotalPrice,

        //        })
        //        .ToList();

        //    return productsellList;

        //}


        public List<VM_Product> sp_ProductSell(DateTime fromDate, DateTime toDate, int storeId)
        {
            var productsellList = context.sp_ProductSellFinal(fromDate, toDate, storeId)
                .Select(a => new VM_Product()
                {

                    ProductId = (int)a.ProductId,
                    ProductName = a.ProductName,
                    Quantity = (decimal)a.Quantity,
                    Unit = a.Unit,
                    UnitPrice = (decimal)a.UnitPrice,
                    TotalPrice = (decimal)a.TotalPrice,

                })
                .ToList();

            return productsellList;

        }


        public List<VM_Product> sp_MainStoreProductStatus(int storeId)
        {
            var productList = context.sp_MainStoreProductStatusQuery(storeId)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    Quantity = (decimal)a.AvailableQuantity,
                    //ProductionCost = a.ProductionCost,
                    //TotalProductionCost = (decimal)a.TotalProductionCost
                    


                })
                .ToList();

            return productList;

        }


    


      

        //public List<VM_Product> sp_SellPointProductStatus(int storeId, DateTime fromDate)
        public List<VM_Product> sp_SellPointProductStatus(int storeId)
        {
            //var productList = context.sp_SellPointProductStatus(storeId, fromDate)
            var productList = context.sp_SellPointProductStatus(storeId)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    Quantity = (decimal)a.AvailableQuantity,
                    UnitPrice = (decimal)a.UnitPrice,
                    TotalPrice = (decimal)a.TotalPrice
                })
                .ToList();

            return productList;

        }

        public List<VM_Product> sp_PuschaseableProductStatusInProductionHouse(int storeId)
        {
            var productList = context.sp_PuschaseableProductStatusInProductionHouse(storeId)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    Quantity = (decimal)a.AvailableQuantity,
                    //UnitPrice = (decimal)a.UnitPrice,
                    //TotalPrice = (decimal)a.TotalPrice
                })
                .ToList();

            return productList;

        }

        public List<VM_Product> sp_SellableProductStatusInProductionHouse(int storeId)
        {
            var productList = context.sp_SellableProductStatusInProductionHouse(storeId)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    Quantity = (decimal)a.AvailableQuantity,
                    UnitPrice = (decimal)a.UnitPrice,
                    TotalPrice = (decimal)a.TotalPrice
                })
                .ToList();

            return productList;

        }


        public List<VM_Product> sp_ProductSellHistoryWithOpeningProductForDayShift(int storeId, int shiftId, DateTime fromDate)
        {
            var productList = context.sp_ProductSellHistoryWithOpeningProductForDayShiftQueryB(storeId, shiftId,fromDate)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    UnitPrice = (decimal)a.UnitPrice,
                    TotalPrice = (decimal)a.TotalPrice,
                    OpeningProduct = (decimal)a.OpeningProduct,
                    InProduct = (decimal)a.Issue,
                    TotalProduct = (decimal)a.TotalProduct,
                    SellProduct = (decimal)a.SellProduct,
                    ClosingProduct = (decimal)a.ClosingProduct,
                    ProductionCost = a.ProductionCost,
                    TotalProductionCost = (decimal)a.TotalProductionCost
                    
                })
                .ToList();

            return productList;

        }


        public List<VM_Product> sp_ProductSellHistoryWithOpeningProductForNightShift(int storeId, int shiftId, DateTime fromDate)
        {
            var productList = context.sp_ProductSellHistoryWithOpeningProductForNightShiftQueryB(storeId, shiftId, fromDate)
                .Select(a => new VM_Product()
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    UnitPrice = (decimal)a.UnitPrice,
                    TotalPrice = (decimal)a.TotalPrice,
                    OpeningProduct = (decimal)a.OpeningProduct,
                    InProduct = (decimal)a.Issue,
                    TotalProduct = (decimal)a.TotalProduct,
                    SellProduct = (decimal)a.SellProduct,
                    ClosingProduct = (decimal)a.ClosingProduct,
                    ProductionCost = a.ProductionCost,
                    TotalProductionCost = (decimal)a.TotalProductionCost
                })
                .ToList();

            return productList;

        }

        public List<VM_OtherExpense> sp_OtherExpenseAmountForSellReportWithOpeningProduct(int storeId, int shiftId, DateTime fromDate)
        {
            var expenseList = context.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId, fromDate)
                .Select(a => new VM_OtherExpense()
                {
                    Less = a.Less,
                    Due = a.Due,
                    Damage = a.Damage,
                    Compliment = a.Compliment
                })
                .ToList();

            return expenseList;

        }


      


        public List<VM_Product> sp_AvailableQuantityForProductToProductionHouse(int storeId, int productId)
        {
            var productsellList = context.sp_AvailableQuantityForProductToProductionHouse(storeId, productId)
                .Select(a => new VM_Product()
                {

                    ProductId = (int) a.ProductId,
                    ProductName = a.ProductName,
                    Quantity = (decimal)a.AvailableQuantity,

                })
                .ToList();

            return productsellList;

        }

        public List<VM_Product> sp_AvailableQuantityForPhToSpTransfer(int storeId, int productId)
        {
            var productsellList = context.sp_AvailableQuantityForPhToSpTransfer(storeId, productId)
                .Select(a => new VM_Product()
                {

                    ProductId = (int)a.ProductId,
                    ProductName = a.ProductName,
                    Quantity = (decimal)a.AvailableQuantity,

                })
                .ToList();

            return productsellList;

        }

        public List<VM_Product> sp_PurchaseAndBothTypeProductListForProductUsesInProductionHouse(int storeId)
        {
            var productsellList = context.sp_PurchaseAndBothTypeProductListForProductUsesInProductionHouse(storeId)
                .Select(a => new VM_Product()
                {

                    ProductId = (int)a.ProductId,
                    ProductName = a.ProductName,
                    Unit = a.Unit,
                    UnitPrice = (decimal) a.UnitPrice,
                    AvilableQuatity = (decimal)a.AvailableQuantity
                    

                })
                .ToList();

            return productsellList;

        }

        public List<VM_Product> sp_AvailableQuantityForProductUsesInProductionHouse(int storeId, int productId)
        {
            var productsellList = context.sp_AvailableQuantityForProductUsesInProductionHouse(storeId, productId)
                .Select(a => new VM_Product()
                {

                    ProductId = (int)a.ProductId,
                    ProductName = a.ProductName,
                    Quantity = (decimal)a.AvailableQuantity,

                })
                .ToList();

            return productsellList;

        }

        public List<VM_Product> sp_ProductSoldForDayShift(int storeId, int shiftId, DateTime fromDate)
        {
            var productList = context.sp_ProductSoldForDayShift(storeId, shiftId, fromDate)
                .Select(a => new VM_Product
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    UnitPrice = (decimal)a.UnitPrice,
                    AvailableQuantity = (decimal)a.AvailableProduct
                    
                })
                .ToList();

            return productList;

        }

        public List<VM_Product> sp_ProductSoldForNightShift(int storeId, int shiftId, DateTime fromDate)
        {
            var productList = context.sp_ProductSoldForNightShift(storeId, shiftId, fromDate)
                .Select(a => new VM_Product
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    UnitPrice = (decimal)a.UnitPrice,
                    AvailableQuantity = (decimal)a.AvailableProduct

                })
                .ToList();

            return productList;

        }


        public List<VM_Product> sp_CheckAvailableQuantityForProductForProductSoldForDayShift(int storeId, int shiftId, int productId, DateTime fromDate)
        {
            var productList = context.sp_CheckAvailableQuantityForProductForProductSoldForDayShift(storeId, shiftId, productId, fromDate)
                .Select(a => new VM_Product
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    UnitPrice = (decimal)a.UnitPrice,
                    AvailableQuantity = (decimal)a.AvailableProduct

                })
                .ToList();

            return productList;

        }



        public List<VM_Product> sp_CheckAvailableQuantityForProductForProductSoldForNightShift(int storeId, int shiftId,int productId, DateTime fromDate)
        {
            var productList = context.sp_CheckAvailableQuantityForProductForProductSoldForNightShift(storeId, shiftId,productId, fromDate)
                .Select(a => new VM_Product
                {
                    ProductName = a.ProductName,
                    ProductId = (int)a.ProductId,
                    Unit = a.Unit,
                    UnitPrice = (decimal)a.UnitPrice,
                    AvailableQuantity = (decimal)a.AvailableProduct

                })
                .ToList();

            return productList;

        }


        public List<VM_Chart> sp_weeklyTotalProductSellByGraph()
        {
            var productList = context.sp_weeklyTotalProductSellByGraph()
                .Select(a => new VM_Chart
                {
                    Day=a.DayOfWeek,
                    Value = (decimal) a.TotalSell

                })
                .ToList();

            return productList;

        }

        public void sp_InsertTotalAmountDailyByShiftWise()
        {
            context.sp_InsertTotalAmountDailyByShiftWise();

        }

        public void sp_InsertTotalAmountDailyForNightShift()
        {
            context.sp_InsertTotalAmountDailyForNightShift();

        }

        public List<VM_Chart> sp_monthlyTotalAmountForDayShift()
        {
            var productList = context.sp_monthlyTotalAmountForDayShift()
                .Select(a => new VM_Chart
                {
                    DayForMonth = a.day,
                    Value = (decimal)a.TotalAmount

                })
                .ToList();

            return productList;

        }

        public List<VM_Chart> sp_monthlyTotalAmountForNightShift()
        {
            var productList = context.sp_monthlyTotalAmountForNightShift()
                .Select(a => new VM_Chart
                {
                    DayForMonth = a.day,
                    Value = (decimal)a.TotalAmount

                })
                .ToList();

            return productList;

        }

          public List<VM_Chart> sp_monthlyTotalAmount()
        {
            var productList = context.sp_monthlyTotalAmount()
                .Select(a => new VM_Chart
                {
                    DayForMonth = a.day,
                    Value = (decimal)a.TotalAmount

                })
                .ToList();

            return productList;

        }


          public List<VM_Chart> sp_weeklyTotalAmountDayShiftWise()
          {
              var productList = context.sp_weeklyTotalAmountDayShiftWise()
                  .Select(a => new VM_Chart
                  {
                      DayOfWeek = a.DayOFWeek,
                      Value = (decimal)a.TotalAmount

                  })
                  .ToList();

              return productList;

          }

          public List<VM_Chart> sp_weeklyTotalAmountNightShiftWise()
          {
              var productList = context.sp_weeklyTotalAmountNightShiftWise()
                  .Select(a => new VM_Chart
                  {
                      DayOfWeek = a.DayOFWeek,
                      Value = (decimal)a.TotalAmount

                  })
                  .ToList();

              return productList;

          }
          public List<VM_Chart> sp_weeklyTotalAmount()
          {
              var productList = context.sp_weeklyTotalAmount()
                  .Select(a => new VM_Chart
                  {
                      DayOfWeek = a.DayOFWeek,
                      Value = (decimal)a.TotalAmount

                  })
                  .ToList();

              return productList;

          }

        
        UnitOfWork unitOfWork = new UnitOfWork();
        public string GetNewVoucherNumber(string type)
        {
            
            int maxVoucherNumber = 100000;
            var voucherNoList = unitOfWork.AccVoucherEntryRepository.Get().Where(a => a.VNumber.Contains(type)).Select(a => a.VNumber).ToList();

            if (voucherNoList.Count > 0)
            {
                List<int> voucherNumberList = new List<int>();
                foreach (var voucherNo in voucherNoList)
                {
                    var data = voucherNo.Split('_');
                    var id = Convert.ToInt32(data[1]);
                    voucherNumberList.Add(id);
                }
                maxVoucherNumber = voucherNumberList.Max();
            }
            return type + "_" + (maxVoucherNumber + 1);
        }

        
        //=====================END==========================//
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
