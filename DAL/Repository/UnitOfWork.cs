using System;

namespace DAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        private RestaurantEntities context = new RestaurantEntities();

        //All tables
        private GenericRepository<tblRestaurantUser> adminUserRepository;
        private GenericRepository<AspNetUser> aspNetUserRepository;
        private GenericRepository<AspNetUserLogin> aspNetUserLoginRepository;
        private GenericRepository<tblRestaurantInformation> restaurantRepository;
        private GenericRepository<tblModule> moduleRepository;
        private GenericRepository<tblAction> actionRepository;
        private GenericRepository<tblUserActionMapping> actionMappingRepository;
        private GenericRepository<tblRestaurantUser> restaurentUserRepository;  
        private GenericRepository<tblProductType> productTypeRepository;
        private GenericRepository<tblProductInformation> productRepository;
        private GenericRepository<tblStoreInformation> storeRepository;
        private GenericRepository<tblProductionHouseInformation> productionHouseInformation;
        private GenericRepository<tblSellsPoint> sellsPointRepository; 
        private GenericRepository<tblSuppliersProduct> suppliersProductRepository;
        private GenericRepository<tblSupplierInformation> suppliersInformationRepository;
        private GenericRepository<tblProductionHouseToProductMapping> productionHouseToProductMappingRepository;
        private GenericRepository<tblProductEntryToProductionHouse> productEntryToProductionHouseRepository;
        private GenericRepository<tblMeasurementUnit> measurementUnitRepository;
        private GenericRepository<tblProductTransfer> productTransferRepository;
        private GenericRepository<tblProductFromSupplier> productFromSupplierRepository;
        private GenericRepository<tblDesignation> designationRepository;
        private GenericRepository<tblEmployeeInformation> employeeInformationRepository;
        private GenericRepository<tblPHtoSPProductTransfer> pHToSPProductTransferRepository;
        private GenericRepository<tblChalanReport> chalanReport;
        private GenericRepository<tblProductSold> productSoldRepository; 
        private GenericRepository<acc_Nature> accNatureRepository; 
        private GenericRepository<acc_Group> accGroupRepository;
        private GenericRepository<acc_VoucherEntry> accVoucherEntryRepository; 
        private GenericRepository<acc_VoucherDetail> accVoucherDetailsRepository; 
        private GenericRepository<acc_Ledger> accLedgerRepository;
        private GenericRepository<tblGroupForShift> groupForShiftRepository; 
        private GenericRepository<tblShift> shiftRepository;
        private GenericRepository<tblOtherExpense> otherExpenseRepository;
        private GenericRepository<tblGroupAndShiftMapping> groupAndShiftMappingRepository; 


        //======================custom repository====================//
        private SPRepository spRepository;
        public SPRepository SPRepository
        {
            get
            {

                if (this.spRepository == null)
                {
                    this.spRepository = new SPRepository(context);
                }
                return spRepository;
            }
        }
        //====================end of custom repository==================//

       
        //======================custom repository====================//
        private CustomRepository customRepository;
        public CustomRepository CustomRepository
        {
            get
            {

                if (this.customRepository == null)
                {
                    this.customRepository = new CustomRepository(context);
                }
                return customRepository;
            }
        }
        //====================end of custom repository==================//
        public GenericRepository<acc_Ledger> AccLedgerRepository
        {
            get
            {

                if (this.accLedgerRepository == null)
                {
                    this.accLedgerRepository = new GenericRepository<acc_Ledger>(context);
                }
                return accLedgerRepository;
            }
        }

        public GenericRepository<tblPHtoSPProductTransfer> PHToSPProductTransferRepository
        {
            get
            {

                if (this.pHToSPProductTransferRepository == null)
                {
                    this.pHToSPProductTransferRepository = new GenericRepository<tblPHtoSPProductTransfer>(context);
                }
                return pHToSPProductTransferRepository;
            }
        }

        public GenericRepository<tblChalanReport> ChalanReport
        {
            get
            {

                if (this.chalanReport == null)
                {
                    this.chalanReport = new GenericRepository<tblChalanReport>(context);
                }
                return chalanReport;
            }
        }

        public GenericRepository<tblProductTransfer> ProductTransferRepository
        {
            get
            {
                if (this.productTransferRepository == null)
                {
                    this.productTransferRepository = new GenericRepository<tblProductTransfer>(context);
                }
                return productTransferRepository;
            }
        }

        public GenericRepository<tblRestaurantUser> AdminUserRepository
        {
            get
            {
                if (this.adminUserRepository == null)
                {
                    this.adminUserRepository = new GenericRepository<tblRestaurantUser>(context);
                }
                return adminUserRepository;
            }
        }

        public GenericRepository<tblProductSold> ProductSoldRepository
        {
            get
            {
                if (this.productSoldRepository == null)
                {
                    this.productSoldRepository = new GenericRepository<tblProductSold>(context);
                }
                return productSoldRepository;
            }
        }


        public GenericRepository<tblProductionHouseToProductMapping> ProductionHouseToProductMappingRepository
        {
            get
            {
                if (this.productionHouseToProductMappingRepository == null)
                {
                    this.productionHouseToProductMappingRepository = new GenericRepository<tblProductionHouseToProductMapping>(context);
                }
                return productionHouseToProductMappingRepository;
            }
        }
        public GenericRepository<tblProductEntryToProductionHouse> ProductEntryToProductionHouseRepository
        {
            get
            {
                if (this.productEntryToProductionHouseRepository == null)
                {
                    this.productEntryToProductionHouseRepository = new GenericRepository<tblProductEntryToProductionHouse>(context);
                }
                return productEntryToProductionHouseRepository;
        }
        }
        public GenericRepository<AspNetUser> AspNetUserRepository
        {
            get
            {

                if (this.aspNetUserRepository == null)
                {
                    this.aspNetUserRepository = new GenericRepository<AspNetUser>(context);
                }
                return aspNetUserRepository;
            }
        }
        public GenericRepository<tblMeasurementUnit> MeasurementUnitRepository
        {
            get
            {

                if (this.measurementUnitRepository == null)
                {
                    this.measurementUnitRepository = new GenericRepository<tblMeasurementUnit>(context);
                }
                return measurementUnitRepository;
            }
        }

        public GenericRepository<AspNetUserLogin> AspNetUserLoginRepository
        {
            get
            {

                if (this.aspNetUserLoginRepository == null)
                {
                    this.aspNetUserLoginRepository = new GenericRepository<AspNetUserLogin>(context);
                }
                return aspNetUserLoginRepository;
            }
        }

        public GenericRepository<tblRestaurantInformation> RestaurantRepository
        {
            get
            {

                if (this.restaurantRepository == null)
                {
                    this.restaurantRepository = new GenericRepository<tblRestaurantInformation>(context);
                }
                return restaurantRepository; ;
            }
        }
        public GenericRepository<tblAction> ActionRepository
        {
            get
            {

                if (this.actionRepository == null)
                {
                    this.actionRepository = new GenericRepository<tblAction>(context);
                }
                return actionRepository;
            }
        }
        public GenericRepository<tblUserActionMapping> UserActonMappingRepository
        {
            get
            {

                if (this.actionMappingRepository == null)
                {
                    this.actionMappingRepository = new GenericRepository<tblUserActionMapping>(context);
                }
                return actionMappingRepository;
            }
        }
        public GenericRepository<tblModule> ModuleRepository
        {
            get
            {

                if (this.moduleRepository == null)
                {
                    this.moduleRepository = new GenericRepository<tblModule>(context);
                }
                return moduleRepository;
            }
        }



        public GenericRepository<tblProductInformation> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<tblProductInformation>(context);
                }
                return productRepository;
            }
        }
        public GenericRepository<tblProductType> ProductTypeRepository
        {
            get
            {

                if (this.productTypeRepository == null)
                {
                    this.productTypeRepository = new GenericRepository<tblProductType>(context);
                }
                return productTypeRepository;
            }
        }

        public GenericRepository<tblStoreInformation> StoreRepository
        {
            get
            {

                if (this.storeRepository == null)
                {
                    this.storeRepository = new GenericRepository<tblStoreInformation>(context);
                }
                return storeRepository;
            }
        }
        //################### PRODUCTION HOUSE  INFORMATION  (MOSADDIK ,SAAD ) #########################

        public GenericRepository<tblProductionHouseInformation> ProductionHouseInformationRepository
        {
            get
            {

                if (this.productionHouseInformation == null)
                {
                    this.productionHouseInformation = new GenericRepository<tblProductionHouseInformation>(context);
                }
                return productionHouseInformation;
        }
        }

        public GenericRepository<tblSuppliersProduct> SuppliersProductRepository
        {
            get
            {

                if (this.suppliersProductRepository == null)
                {
                    this.suppliersProductRepository = new GenericRepository<tblSuppliersProduct>(context);
                }
                return suppliersProductRepository;
        }
        }
        public GenericRepository<tblDesignation> DesignationRepository
        {
            get
            {

                if (this.designationRepository == null)
                {
                    this.designationRepository = new GenericRepository<tblDesignation>(context);
                }
                return designationRepository;
            }
        }

        public GenericRepository<tblSupplierInformation> SuppliersInformationRepository
        {
            get
            {

                if (this.suppliersInformationRepository == null)
                {
                    this.suppliersInformationRepository = new GenericRepository<tblSupplierInformation>(context);
                }
                return suppliersInformationRepository;
            }
        }
  
        public GenericRepository<tblRestaurantUser> RestaurantUserRepository
        {
            get
            {

                if (this.restaurentUserRepository == null)
                {
                    this.restaurentUserRepository = new GenericRepository<tblRestaurantUser>(context);
                }
                return restaurentUserRepository;
            }
        }
        public GenericRepository<tblSellsPoint> SellsPointRepository
        {
            get
            {

                if (this.sellsPointRepository == null)
                {
                    this.sellsPointRepository = new GenericRepository<tblSellsPoint>(context);
                }
                return sellsPointRepository;
            }
        }


        public GenericRepository<tblEmployeeInformation> EmployeeInformationRepository
        {
            get
            {
                if (this.employeeInformationRepository == null)
                {
                    this.employeeInformationRepository = new GenericRepository<tblEmployeeInformation>(context);
                }
                return employeeInformationRepository;
            }
        }

        public GenericRepository<tblProductFromSupplier> ProductFromSupplierRepository
        {
            get
            {

                if (this.productFromSupplierRepository == null)
                {
                    this.productFromSupplierRepository = new GenericRepository<tblProductFromSupplier>(context);
                }
                return productFromSupplierRepository;
            }
        }

        public GenericRepository<acc_Nature> AccNatureRepository
        {
            get
            {

                if (this.accNatureRepository == null)
                {
                    this.accNatureRepository = new GenericRepository<acc_Nature>(context);
                }
                return accNatureRepository;
            }
        }

        public GenericRepository<acc_Group> AccGroupRepository
        {
            get
            {

                if (this.accGroupRepository == null)
                {
                    this.accGroupRepository = new GenericRepository<acc_Group>(context);
                }
                return accGroupRepository;
            }
        }
         
         
        public GenericRepository<acc_VoucherEntry> AccVoucherEntryRepository
        {
            get
            {

                if (this.accVoucherEntryRepository == null)
                {
                    this.accVoucherEntryRepository = new GenericRepository<acc_VoucherEntry>(context);
                }
                return accVoucherEntryRepository;
            }
        }
        public GenericRepository<acc_VoucherDetail> AccVoucherDetailsRepository
        {
            get
            {

                if (this.accVoucherDetailsRepository == null)
                {
                    this.accVoucherDetailsRepository = new GenericRepository<acc_VoucherDetail>(context);
                }
                return accVoucherDetailsRepository;
            }
        }
        public GenericRepository<tblGroupForShift> GroupForShiftRepository
        {
            get
            {

                if (this.groupForShiftRepository == null)
                {
                    this.groupForShiftRepository = new GenericRepository<tblGroupForShift>(context);
                }
                return groupForShiftRepository;
            }
        }
        public GenericRepository<tblShift> ShiftRepository
        {
            get
            {

                if (this.shiftRepository == null)
                {
                    this.shiftRepository = new GenericRepository<tblShift>(context);
                }
                return shiftRepository;
            }
        }
        public GenericRepository<tblOtherExpense> OtherExpenseRepository
        {
            get
            {

                if (this.otherExpenseRepository == null)
                {
                    this.otherExpenseRepository = new GenericRepository<tblOtherExpense>(context);
                }
                return otherExpenseRepository;
            }
        }
        public GenericRepository<tblGroupAndShiftMapping> GroupAndShiftMappingRepository
        {
            get
            {

                if (this.groupAndShiftMappingRepository == null)
                {
                    this.groupAndShiftMappingRepository = new GenericRepository<tblGroupAndShiftMapping>(context);
                }
                return groupAndShiftMappingRepository;
            }
        }
        
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
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