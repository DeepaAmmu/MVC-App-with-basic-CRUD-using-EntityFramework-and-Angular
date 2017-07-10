using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F2BMVCAngularApp.Models;
using System.Data;
using System.Net;
using System.Text;

namespace F2BMVCAngularApp.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: /Login/ 

        public ActionResult Login()
        {
            return View();
        }


        // GET: /Employee/

        public ActionResult Employee()
        {

            EmployeeModel em = new EmployeeModel();
            List<EmployeeDetail> empDetail = db.EmployeeDetails.ToList();



            foreach (var detail in empDetail)
            {
                em.EmployeeDetails.Add(new EmployeeViewModel()
                {
                    Id = detail.Id,
                    Name = detail.Name,
                    Number = detail.Number,
                    PhoneNumber = detail.PhoneNumber,
                    EmailId = detail.EmailId,
                    AddressID = detail.EmployeeAddress.AddressID,
                    FlatNo = detail.EmployeeAddress.FlatNo,
                    street = detail.EmployeeAddress.street,
                    Country = detail.EmployeeAddress.Country,
                    Pincode = detail.EmployeeAddress.Pincode,
                    CityName = detail.EmployeeAddress.city.CityName,
                    StateName = detail.EmployeeAddress.State.StateName,
                    CityId = detail.EmployeeAddress.CityID,
                    StateId = detail.EmployeeAddress.StateID
                    //AddressID = GetAddress(detail.Id)
                    //AddressID = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).AddressID,
                    //FlatNo = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).FlatNo,
                    //Country = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).Country,
                    //Pincode = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).Pincode,
                    //street = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).street,
                    //CityId = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).city.CityId,
                    //StateId = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).State.StateId,
                    //CityName = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).city.CityName,
                    //StateName = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == detail.Id).State.StateName

                });

            }

            return View(em);
        }

        private int GetAddress(int Id)
        {

            var resultquery = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).AddressID;
            var FlatNo = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).FlatNo;
            var Country = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).Country;
            var Pincode = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).Pincode;
            var street = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).street;
            var CityId = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).city.CityId;
            var StateId = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).State.StateId;
            var CityName = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).city.CityName;
            var StateName = db.EmployeeAddresses.FirstOrDefault(x => x.AddressID == Id).State.StateName;


            return resultquery;
        }

        private string GetStateName(int StateId)
        {
            var result = db.States.Where(x => x.StateId == StateId).Select(x => x.StateName);
            return result.ToString();
        }

        private string GetCityName(int CityId)
        {
            var result = db.cities.Where(x => x.CityId == CityId).Select(x => x.CityName);
            return result.ToString();
        }

        //
        // GET: /detail/Create

        public ActionResult Create()
        {
            using (EmployeeContext db = new EmployeeContext())
            {
                ViewBag.Statelist = db.States.ToList();

            }
            return View();
        }

        //
        // POST: /detail/Create

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection, EmployeeViewModel employeedetail)
        {
            var citySelectedValue = collection["CityDropDownList"];
            
            //Char delimiter = ':';
            //String[] substrings = citySelectedValue.Split(delimiter);
            employeedetail.CityId = Convert.ToInt32(citySelectedValue);

            if (ModelState.IsValid)
            {
                EmployeeDetail emp = new EmployeeDetail();
                EmployeeAddress empAdd = new EmployeeAddress();

                emp.Name = employeedetail.Name;
                emp.Number = employeedetail.Number;
                emp.EmailId = employeedetail.EmailId;
                emp.PhoneNumber = employeedetail.PhoneNumber;

                //insert EmployeeDetails table
                db.EmployeeDetails.Add(emp);
                db.SaveChanges();

                //get employee id
                empAdd.AddressID = emp.Id;

                empAdd.FlatNo = employeedetail.FlatNo;
                empAdd.street = employeedetail.street;
                empAdd.CityID = employeedetail.CityId;
                empAdd.StateID = employeedetail.StateId;
                empAdd.Country = employeedetail.Country;
                empAdd.Pincode = employeedetail.Pincode;

                //insert EmployeeAddress table
                db.EmployeeAddresses.Add(empAdd);
                db.SaveChanges();
                return RedirectToAction("Employee");
            } return View(employeedetail);
        }

        [HttpPost]
        public JsonResult GetCityList(int StateId)
        {
            var citylist = new SelectList(db.cities.Where(x => x.StateID == StateId).ToList(), "CityId", "CityName");
            return Json(citylist);
        }


        // GET: EmployeeDetail/Edit/5
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Statelist = db.States.ToList();
            
            EmployeeViewModel evm = new EmployeeViewModel();
            EmployeeDetail employeedetail = db.EmployeeDetails.Find(id);

            if (employeedetail == null)
            {
                return HttpNotFound();
            }

            
            evm.Id = employeedetail.Id;
            evm.Name = employeedetail.Name;
            evm.Number = employeedetail.Number;
            evm.EmailId = employeedetail.EmailId;
            evm.PhoneNumber = employeedetail.PhoneNumber;
            evm.AddressID = employeedetail.Id;

            evm.FlatNo = employeedetail.EmployeeAddress.FlatNo;
            evm.street = employeedetail.EmployeeAddress.street;
            evm.CityId = employeedetail.EmployeeAddress.city.CityId;
            evm.StateId = employeedetail.EmployeeAddress.State.StateId;
            evm.Country = employeedetail.EmployeeAddress.Country;
            evm.Pincode = employeedetail.EmployeeAddress.Pincode;
            evm.CityName = employeedetail.EmployeeAddress.city.CityName;
            evm.StateName = employeedetail.EmployeeAddress.State.StateName;
            ViewBag.selectedstate = employeedetail.EmployeeAddress.State.StateId;
            ViewBag.selectedcity = employeedetail.EmployeeAddress.city.CityId;
            return View(evm);
        }

        // POST: EmployerDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection,EmployeeDetail empDetail, EmployeeAddress empAddress)
        {
            var citySelectedValue = collection["CityDropDownList"];
            //Char delimiter = ':';
            //String[] substrings = citySelectedValue.Split(delimiter);
            empAddress.CityID = Convert.ToInt32(citySelectedValue);
            if (ModelState.IsValid)
            {
                db.Entry(empDetail).State = EntityState.Modified;
                db.Entry(empAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Employee");
            }
            return View(empDetail);
        }




        // POST: EmployeeDetails/Delete/5
        [HttpPost]
        ////[ValidateAntiForgeryToken] called using ajax actionlink 
        public ActionResult Delete(int id = 0)
        {
            EmployeeDetail empDetail = db.EmployeeDetails.Find(id);
            EmployeeAddress empAddressDetail = db.EmployeeAddresses.Find(id);
            db.EmployeeAddresses.Remove(empAddressDetail);

            //due to foreign key constraint employee address is  getting deleted first

            db.EmployeeDetails.Remove(empDetail);
            
            db.SaveChanges();
            return RedirectToAction("Employee");
        }

    }
}

