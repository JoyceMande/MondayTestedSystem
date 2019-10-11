using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCRIPTERS.Core.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using LinqToExcel;
using SCRIPTERS.Models;
using System.Data.SqlClient;


namespace SCRIPTERS.Controllers
{
    public class ImportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadExcel()
        {

            return View();
        }

        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }

        [HttpPost]
        public ActionResult UploadExcel(Customer customers, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<Customer>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {

                        try
                        {
                            if (a.Name != "")
                            {
                                Customer TU = new Customer();
                                TU.Code = a.Code;
                                TU.Name = a.Name;
                                TU.Email = a.Email;
                                TU.ContactNo = a.ContactNo;
                                TU.Address = a.Address;
                                db.Customers.Add(TU);

                                db.SaveChanges();
                                ViewBag.Success = "File Uploaded Successfully. Open Customer List to review details";



                            }
                            else
                            {
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {
                            RedirectToAction("List", "Customers");
                           
                        }
                    }
                }
                else
                {
                    //alert message for invalid file format 
                     ViewBag.Error = "Only Excel File allowed";
                }
                //deleting excel file from folder  
                //using (System.IO.File.Exists(pathToExcelFile)) { }
                //if ((System.IO.File.Exists(pathToExcelFile)))
                //{
                //    System.IO.File.Delete(pathToExcelFile);
                //}
                RedirectToAction("List", "Customers");
                //return Json( JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Error = "Please Upload a File";
               
            }
            return View("Index");
            
        }
        


    }
}
