using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Restaurant.Models;
using System.Data.Entity;
using Restaurant.Utility;
using DAL;
using DAL.Repository;
using Restaurant.Models.ViewModel;

namespace Restaurant.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            //return View();
            return PartialView();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            TempData["errorMessage"] = string.Empty;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);

                    using (RestaurantEntities rc = new RestaurantEntities())
                    {
                        try
                        {
                            tblRestaurantUser RestaurantUser = new tblRestaurantUser();
                            var resturant = unitOfWork.RestaurantRepository.Get().Where(x => x.short_name == model.RestaurantName).FirstOrDefault();
                            RestaurantUser.UserId = user.Id;                            
                            RestaurantUser.is_loggedIn = 1;
                            RestaurantUser.last_logged_time = DateTime.Now;
                            RestaurantUser.restaurant_id = resturant.restaurant_id;
                            //rc.tblAdminUsers.Attach(RestaurantUser);
                            //var entry = rc.Entry(RestaurantUser);
                            //entry.State = EntityState.Modified;
                            //rc.SaveChanges();
                            unitOfWork.AdminUserRepository.Update(RestaurantUser);
                            unitOfWork.Save();
                            SessionManger.SetLoggedInTime(Session, DateTime.Now);

                            SessionManger.SetLoggedInUser(Session, model.UserName, resturant.restaurant_id);
                            return Redirect("/#Index");
                           // return Json(new { success = true, successMessage = "Successfully User Created" });

                        }
                        catch (Exception ex)
                        {
                            return Redirect("/#Login");
                            throw;

                        }
                       
                    }

                    //return RedirectToLocal(returnUrl);
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    TempData["errorMessage"] = "Invalid username or password.";
                    //return View(model); View(model);
                    //System.Threading.Thread.SpinWait(60); 
                    return Redirect("/#Login");
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(model);
            TempData["errorMessage"] = "Invalid username or password.";
            //return View(model); View(model);
            //System.Threading.Thread.SpinWait(60); 
            return Redirect("/#Login");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return PartialView();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var results = await UserManager.FindByNameAsync(model.UserName);
                if (results != null)
                {
                    return Json(new { success = false, errorMessage = "This User Name already exists" }, JsonRequestBehavior.AllowGet);
                }
                else 
                {
                    // Create User
                    var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    RestaurantEntities RestaurantCal = new RestaurantEntities();
                    AspNetUser anu = new AspNetUser();

                    //AspNetUser userId = RestaurantCal.AspNetRoles.Select(a => a.Name == model.UserName).FirstOrDefault();
                    AspNetUser searchUser = RestaurantCal.AspNetUsers.Where(a => a.UserName == model.UserName).FirstOrDefault();
                    decimal restaurentId = SessionManger.RestaurantOfLoggedInUser(Session);
                    if (result.Succeeded)
                    {                       
                                             
                        using ( RestaurantEntities rc = new RestaurantEntities())
                        {
                            try
                            {
                                tblRestaurantUser RestaurantUser = new tblRestaurantUser();


                                RestaurantUser.UserId = searchUser.Id;
                                RestaurantUser.restaurant_id = restaurentId;
                                RestaurantUser.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                                RestaurantUser.CreatedBy = SessionManger.LoggedInUser(Session);
                                RestaurantUser.CreatedDateTime = DateTime.Now;
                                RestaurantUser.EditedBy = null;
                                RestaurantUser.EditedDateTime = null;
                               // RestaurantUser.is_admin = 0;
                                //RestaurantUser.is_loggedIn = null;
                                //RestaurantUser.last_logged_time = null;
                                //RestaurantUser.approve_by = null;
                                //RestaurantUser.approve_date = null;
                                rc.tblRestaurantUsers.Add(RestaurantUser);
                                rc.SaveChanges();

                                return Json(new { success = true, successMessage = "User has created Successfully." });
                               
                            }
                            catch (Exception)
                            {                                
                                throw;
                            }                         
                        }  
                    }
                    else
                    {                     
                        return Json(new { success = false, errorMessage = "This Email has already taken."}, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid." }, JsonRequestBehavior.AllowGet);
            }

           
            return View(model);
        }
        //##################### 05/03/17: TODAY'S WORK START'S HERE By Mamun & Sumon ################################################################
       
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult AddAspNetUser() 
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult AspNetUserList()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllUser()
        {
            try
            {
                var userList = (from a in unitOfWork.AspNetUserRepository.Get()
                                select new VM_AspNetUser()
                                {
                                    Id = a.Id,
                                    UserFullName = a.UserFullName,
                                    UserName = a.UserName,
                                    PhoneNo = a.PhoneNumber,
                                    Email = a.Email
                                    //Password = a.PasswordHash
                                }).ToList();
                return Json(new { success = true, result = userList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                 return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult DeleteUser(string Id)
        {
            try
            {
                AspNetUser aUser = unitOfWork.AspNetUserRepository.GetByID(Id);
                tblRestaurantUser rUser =
                   unitOfWork.RestaurantUserRepository.Get().Where(x => x.UserId  == Id).FirstOrDefault();
                if(aUser == null)
                {
                    return Json(new { success = false, errorMessage = "User Delete Failed." }, JsonRequestBehavior.AllowGet);
                }
                unitOfWork.RestaurantUserRepository.Delete(rUser);
                unitOfWork.AspNetUserRepository.Delete(aUser);

                unitOfWork.Save();
                return Json(new { success = true, message = "User Delete Successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                 return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //#####################0 05/03/17: TODAY'S WORK ENDS HERE By Mamun & Sumon ####################################################################
        //#####################0 06/03/17: TODAY'S WORK ENDS HERE By Mamun & Sumon ####################################################################
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateUser(VM_AspNetUser aUser)
        {
            AspNetUser aspNetUser=unitOfWork.AspNetUserRepository.GetByID(aUser.Id);
            aspNetUser.Id = aUser.Id;
            aspNetUser.UserFullName = aUser.UserFullName;
            aspNetUser.UserName = aUser.UserName;
            aspNetUser.PhoneNumber = aUser.PhoneNo;
            aspNetUser.Email = aUser.Email;
            tblRestaurantUser aRestaurantUser =
                unitOfWork.RestaurantUserRepository.Get(a => a.UserId == aUser.Id).FirstOrDefault();
            aRestaurantUser.EditedBy = SessionManger.LoggedInUser(Session);
            aRestaurantUser.EditedDateTime = DateTime.Now;
            //PASSOWRD save+ UserName & Email uniQ check required

            var EmailExist = unitOfWork.AspNetUserRepository.Get()
                .Where(x => x.Email == aUser.Email && x.Id != aUser.Id).ToList();
            var UserNameExist = unitOfWork.AspNetUserRepository.Get()
           .Where(x => x.UserName == aUser.UserName && x.Id != aUser.Id).ToList();

            try
            {

                if (!EmailExist.Any())
                {
                    if (!UserNameExist.Any())
                    {
                        //UPDATE

                        unitOfWork.RestaurantUserRepository.Update(aRestaurantUser);
                        unitOfWork.AspNetUserRepository.Update(aspNetUser);
                        unitOfWork.Save();
                        return Json(new { success = true, successMessage = "User has updated Successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        
                            return Json(new { success = false, errorMessage = "This user name already exist." }, JsonRequestBehavior.AllowGet);
                       
                    }
                }
                else
                {
                  
                        return Json(new { success = false, errorMessage = "This email already exist." }, JsonRequestBehavior.AllowGet);
                   
                }

            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

        //#####################0 06/03/17: TODAY'S WORK ENDS HERE By Mamun & Sumon ####################################################################
        

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null) 
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
	
        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null) 
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
              
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return PartialView();
        }

        //
        // POST: /Account/Manage
        [HttpPost]

        public async Task<JsonResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            //ViewBag.HasLocalPassword = hasPassword;
            //ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        await SignInAsync(user, isPersistent: false);
                        return Json(new { success = true, successMessage="Password Change Successfully" }, JsonRequestBehavior.AllowGet);
                        //return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = "Old Password Does not Match" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        //return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

        
           
            // return View(model);
              return Json(new { success = false, errorMessage= "Password Can not Be Change" }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");
                        
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            //return RedirectToAction("Index", "Home");
            return Redirect("/#Index");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public JsonResult LoadModules()
        {
            try
            {
                decimal id = SessionManger.RestaurantOfLoggedInUser(Session);
                string user_id = User.Identity.GetUserId();
                var result = unitOfWork.ModuleRepository.Get()
                .Select(a => new
                {
                    a.module_id,
                    a.module_name,
                    a.module_icon,
                    action = a.tblActions.Where(b => b.is_in_menu == true && b.tblUserActionMappings.Where(am => am.user_id == user_id && am.is_permitted == 1).Any())
                    .Select(ac
                        => new
                        {
                            ac.id,
                            ac.name,
                            ac.display_name,
                            ac.url
                        }
                        ).ToList()
                }).Where(a => a.action.Count() > 0).ToList();
                return Json(new { result = result, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult UserActionMapping()
        {
            return View();
        }

        //
        public JsonResult GetAllUserForCurrentRestaurent()
        {

            decimal restaurantId = SessionManger.RestaurantOfLoggedInUser(Session);
            try
            {
                //var users = DropDown.ddlUsers(membershipId);
                var users = unitOfWork.AdminUserRepository.Get().Where(x=>x.restaurant_id == restaurantId).Select(
                a => new
                {
                    text = a.AspNetUser.UserName,
                    value = a.UserId
                }).ToList();

                return Json(new { data = users, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UserMenuActionForPermission(string userId)
        {
            //decimal id = SessionManger.BrokerOfLoggedInUser(Session).membership_id;
            decimal restaurantId = SessionManger.RestaurantOfLoggedInUser(Session);
         
            //string user_id = User.Identity.GetUserId();
            //var views = userActionMappingFactory.GetAll().Where(a => a.user_id == user_id)
            //    .Select(x => x.action_id).ToList();
            var result = unitOfWork.ModuleRepository.Get()
            .Select(a => new
            {
                a.module_id,
                label = a.module_name,
                children = a.tblActions.Where(x => x.is_in_menu == true)
                .Select(ac
                    => new
                    {
                        ac.id,
                        label = ac.display_name,
                        //a.membership_id,
                        selected = ac.tblUserActionMappings.Where(x => x.user_id == userId).Select(x => x.user_id).FirstOrDefault() == null ? false : true
                    }
                    ).ToList()
            }).ToList();

            return Json(new { data = result, success = true }, JsonRequestBehavior.AllowGet);
        }

        [SessionManger.CheckUserSession]
        public JsonResult SaveUserPermission(decimal[] id, string userId)
        {
            //userActionMappingFactory = new UserActionMappingFactory();
            //actionFactory = new ActionFactory();
            try
            {
                List<tblAction> actions = new List<tblAction>();
                actions = unitOfWork.ActionRepository.Get().ToList();

                List<tblUserActionMapping> actionMappings = new List<tblUserActionMapping>();
                actionMappings = unitOfWork.UserActonMappingRepository.Get().Where(a => a.user_id == userId).ToList();

                foreach (var items in actionMappings)
                {
                    unitOfWork.UserActonMappingRepository.Delete(items);
                }

                if (id != null)
                {
                    foreach (var items in id)
                    {
                        tblUserActionMapping actionMapping = new tblUserActionMapping();
                        actionMapping.action_id = Int32.Parse(items.ToString());
                        actionMapping.user_id = userId;
                        actionMapping.is_permitted = 1;
                        actionMapping.restaurant_id = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        unitOfWork.UserActonMappingRepository.Insert(actionMapping);
                        //var selectedAction = actions.Where(a => a.id == items).FirstOrDefault();
                        //if (selectedAction != null)
                        //{
                        //    var hidenactions =
                        //        actions.Where(ha => ha.class_name == selectedAction.class_name && ha.id != items);
                        //    foreach (var hidenAction in hidenactions)
                        //    {
                        //        tblUserActionMapping hactionMapping = new tblUserActionMapping();
                        //        hactionMapping.action_id = hidenAction.id;
                        //        hactionMapping.user_id = userId;
                        //        hactionMapping.is_permitted = 1;
                        //        hactionMapping.membership_id = SessionManger.BrokerOfLoggedInUser(Session).membership_id;
                        //        userActionMappingFactory.Add(hactionMapping);
                        //    }
                        //}
                    }
                }
                unitOfWork.Save();

                return Json(new { success = true, errorMessage = "Mapped Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}