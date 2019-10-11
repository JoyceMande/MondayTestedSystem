using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCRIPTERS.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index(string action)
        {
            ViewBag.Message = action;
            string path;
            switch (action)
            {
                case "login":
                    path = "Howtoregisterandlogin.html";
                    break;
                case "Home":
                    path = "Howtoaddcalendarevent.html";
                    break;
                case "AddbooksCat":
                    path = "HowtoAddBookcategory.html";
                    break;
                case "ManageBooksCat":
                    path = "HowtomanagelistofBookcategory.html";
                    break;
                case "AddInventoryCat":
                    path = "HowtoAddInventorycategory.html";
                    break;
                case "ManageInventoryCat":
                    path = "Howtomanagelistofinventorycatego.html";
                    break;
                case "AddBooks":
                    path = "HowtoAddBook.html";
                    break;
                case "manageBooks":
                    path = "HowtomanagelistofBooks.html";
                    break;
                case "AddInventory":
                    path = "HowtoAddinventory.html";
                    break;
                case "manageInventory":
                    path = "HowtomanagelistofInventory.html";
                    break;
                case "AddOutlet":
                    path = "HowtoAddOutlet.html";
                    break;
                case "manageOutlet":
                    path = "HowtomanagelistofOutlet.html";
                    break;
                case "users":
                    path = "HowtoManageroles.html";
                    break;
                case "AddEmployee":
                    path = "HowtoAddEmployees.html";
                    break;
                case "manageEmployees":
                    path = "HowtomanagelistofEmployees.html";
                    break;
                case "AddSupplier":
                    path = "HowtoAddSupplier.html";
                    break;
                case "manageSupplier":
                    path = "HowtomanagelistofSuppliers.html";
                    break;
                case "AddCustomer":
                    path = "HowtoAddCustomer.html";
                    break;
                case "manageCustomer":
                    path = "HowtomanagelistofCustomers.html";
                    break;
                case "AddPurchase":
                    path = "HowtoAddPurchase.html";
                    break;
                case "managePurchase":
                    path = "HowtomanagelistofPurchases.html";
                    break;
                case "AddOrder":
                    path = "HowtoAddOrder.html";
                    break;
                case "manageOrder":
                    path = "HowtomanagelistofOrders.html";
                    break;
                case "AddBookSale":
                    path = "HowtoAddBooksale.html";
                    break;
                case "manageBookSale":
                    path = "Howtomanagelistofbooksales.html";
                    break;
                case "inventorySale":
                    path = "HowtoAddInventorysale.html";
                    break;
                case "manageInventorySale":
                    path = "HowtomanagelistofInventorysales.html";
                    break;
                case "PurchaseReport":
                    path = "Howtogeneratepurchasereports.html";
                    break;
                case "orderReport":
                    path = "Howtomakeorderreports.html";
                    break;
                case "BookSalesReport":
                    path = "HowtogenerateBookSalesReport.html";
                    break;
                case "InventorySalesReport":
                    path = "HowtogenerateInventorySalesRepor.html";
                    break;
                case "auditTrail":
                    path = "HowtoViewAudittrail.html";
                    break;

                case "email":
                    path = "HowtoSendEmail.html";
                    break;
                default:
                    path = "Welcomepage.html";
                    break;

            }

            return new FilePathResult(path, "text/html");
        }


    }
}
