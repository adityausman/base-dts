using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BASE_RFID_TMS.Models;
using static BASE_RFID_TMS.Models.GlobalVar;

namespace BASE_RFID_TMS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    [GetTimeOutSystem]
    //[Authorize]
    public class ProcessController : ApiController
    {

        // PUT api/documentation
        /// <summary>
        /// Fungsi set Receiving 
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public IHttpActionResult setLetterLooseTire([FromBody]Process.cCrudServiceMaintenance prm)
        //{
        //    var returnRes = Process.InsertServiceMaintenance(prm);

        //    return Json(returnRes);

        //}
        //[HttpPost]
        //public IHttpActionResult setLetterFoundTire([FromBody]Process.cCrudServiceMaintenance prm)
        //{
        //    var returnRes = Process.InsertServiceMaintenance(prm);

        //    return Json(returnRes);

        //}
        private bool IsFileNameSafe(string fileName)
        {
            // Define a list of characters that are not allowed in file names.
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Check if the file name contains any invalid characters.
            if (fileName.IndexOfAny(invalidChars) >= 0)
            {
                return false;
            }

            // Additional checks for path traversal prevention.
            if (fileName.Contains("..") || fileName.Contains("/") || fileName.Contains("\\"))
            {
                return false;
            }

            // If all checks pass, the file name is considered safe.
            return true;
        }
        [HttpPost]
        [Route("api/process/UploadImageLetterStatement/{id}/{type}")]
        public async Task<IHttpActionResult> UploadImageLetterStatement(string id, int type)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Unsupported media type");
            }
            cResponUpload returnRes = new cResponUpload();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            // Set the upload folder path.
            string folder = (type == 0) ? "TyreLoose" : "TyreFound";
            var uploadPath = ConfigurationManager.AppSettings["url_frontend"] + folder;
            uploadPath = Path.Combine(HttpRuntime.AppDomainAppPath, uploadPath);
            // Create the directory if it doesn't exist.
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Create a provider to handle the uploaded file.
            var provider = new MultipartFormDataStreamProvider(uploadPath);
            System.Guid newId = Guid.NewGuid();
            try
            {
                // Read the form data and store the file in the upload folder.
                await Request.Content.ReadAsMultipartAsync(provider);

                // Get the uploaded file.
                var file = provider.FileData.FirstOrDefault();

                if (file == null)
                {
                    returnRes.flag = false;
                    returnRes.status = "No file was uploaded";
                    
                }

                // Get the original file name.
                var originalFileName = file.Headers.ContentDisposition.FileName.Trim('\"');
                if (string.IsNullOrWhiteSpace(originalFileName))
                {
                    return BadRequest("File name is empty or invalid.");
                }
                
                if (IsFileNameSafe(originalFileName))
                {
                    // Create a new file name based on the uploaded file's name.
                    string newFileName = newId + Path.GetExtension(originalFileName);

                    // Get the full path for the new file.
                    string newPath = Path.Combine(uploadPath, newFileName);

                    // Move the file to the new path.
                    File.Move(file.LocalFileName, newPath);

                    returnRes.flag = true;
                }
                else
                {
                    returnRes.flag = false;
                    return BadRequest("Invalid file name");
                }
                
            }
            catch (Exception ex)
            {
                returnRes.flag = false;
                returnRes.message = ex.Message;
                //return InternalServerError(ex);
            }
            if (returnRes.flag == true) {
                returnRes.id = newId;
                returnRes.status = "Success";
            }
            else {
                returnRes.status = "Failed";
            }

            return Json(returnRes);
        }
        //[HttpPost]
        //public IHttpActionResult SaveImageStatementLetter([FromBody]Process.cCrudUploadImageLetter prm)
        //{
        //    //type: 0 = TyreLoose, 1 = TyreFound
        //    string filePath = ConfigurationManager.AppSettings["url_frontend"]; //+"~/img/vehicle/");
        //    string folder = "";

        //    if (prm.type == 0) {
        //        folder = "TyreLoose";
        //    }
        //    else {
        //        folder = "TyreFound";
        //    }
        //    string filePathImg = filePath + "img/"+ folder + "/";
        //    if (prm.file == null)
        //    {
        //        return Json("Image not selected");
        //    }
        //    if (!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }
        //    filePath = filePath + Path.GetFileName(prm.id + ".jpg");
        //    prm.file.SaveAs(filePath);
        //    return Json("success");
        //}

        [HttpPost]
        public IHttpActionResult setServiceMaintenance([FromBody]Process.cCrudServiceMaintenance prm)
        {
            var returnRes = Process.InsertServiceMaintenance(prm);

            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setTyreNonPTSI([FromBody]Process.cCrudSync prm)
        {
            var returnRes = Process.InsertTyreNonPTSI(prm);

            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setSync([FromBody]Process.cCrudSync prm)
        {
            var returnRes = Process.InsertSyncronize(prm);

            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setSyncOfflineActivity([FromBody]Process.cCrudSyncOfflineActivity prm)
        {
            var returnRes = Process.InsertSyncronizeOfflineActivity(prm);

            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setWriteData([FromBody]Process.cCrudWriteData prm)
        {
            var returnRes = Process.InsertWriteData(prm);

            return Json(returnRes);

        }


        [HttpPost]
        public IHttpActionResult setReplaceTag([FromBody]Process.cCrudReplaceTag prm)
        {
            var returnRes = Process.SetReplaceTag(prm);

            return Json(returnRes);

        }

        [HttpPost]
        public IHttpActionResult setChangeEpc([FromBody]Process.cCrudChangeEpc prm)
        {
            var returnRes = Process.SetChangeEpc(prm);

            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setConsignmentReceiving([FromBody]Receiving.cCrudReceiving prm)
        {
            var returnRes = Receiving.InsertConsignmentReceivingProcess(prm);
            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setReceiving([FromBody]Receiving.cCrudReceiving prm)
        {
            var returnRes = Receiving.InsertReceivingProcess(prm);
            return Json(returnRes);

        }

        [HttpPost]
        public IHttpActionResult setReceivingInsertManualSerialNumber([FromBody]Receiving.cCrudReceivingInsertManualSerialNumber prm)
        {
            var returnRes = Receiving.InsertReceivingManualInsertSerialNumberProcess(prm);
            return Json(returnRes);

        }

        [HttpPost]
        public IHttpActionResult setRfidVehicle([FromBody]Process.cVehicle prm)
        {
            var returnRes = Process.InsertRfidVehicle(prm);
            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setFleetKmHm([FromBody]Process.cOdometer prm)
        {
            var returnRes = Process.InsertFleetOdometer(prm);
            return Json(returnRes);

        }
        // PUT api/documentation
        /// <summary>
        /// Fungsi set Registrasi RFID (HH)
        /// </summary>
        /// <returns></returns> 
        [HttpPost]
        public IHttpActionResult setUtilityTireStokInLocation([FromBody]Process.cCrudUtilityTireStokInLocation prm)
        {
            var returnRes = Process.InsertUtilityTireStokInLocation(prm);
            return Json(returnRes);
        }
        [HttpPost]
        public IHttpActionResult setUtilityTireStokInUnitPerFleet([FromBody]Process.cCrudUtilityTireStokInUnitPerFleet prm)
        {
            var returnRes = Process.InsertUtilityTireStokInUnitPerFleet(prm);
            return Json(returnRes);
        }
        [HttpPost]
        public IHttpActionResult setUtilityTireStokInUnitPerVehicle([FromBody]Process.cCrudUtilityTireStokInUnitPerVehicle prm)
        {
            var returnRes = Process.InsertUtilityTireStokInUnitPerVehicle(prm);
            return Json(returnRes);
        }
        [HttpPost]
        public IHttpActionResult setLetterLooseTire([FromBody]Process.cCrudLetterLoose prm)
        {
            var returnRes = Process.InsertLetterLooseTire(prm);
            return Json(returnRes);
        }
        [HttpPost]
        public IHttpActionResult setLetterDismantleTire([FromBody]Process.cCrudLetterLoose prm)
        {
            var returnRes = Process.InsertLetterDismantleTire(prm);
            return Json(returnRes);
        }
        [HttpPost]
        public IHttpActionResult deleteLetterDismantleTire([FromBody]Process.cCrudLetterLoose prm)
        {
            var returnRes = Process.DeleteLetterDismantleTire(prm);
            return Json(returnRes);
        }
        [HttpPost]
        public IHttpActionResult setLetterFoundTire([FromBody]Process.cCrudLetterFound prm)
        {
            var returnRes = Process.InsertLetterFoundTire(prm);
            return Json(returnRes);
        }
        [HttpPost]
        public IHttpActionResult setRegister([FromBody]Register.cCrudRegister prm)
        {
            var returnRes = Register.InsertRegisterProcess(prm);
            return Json(returnRes);

        }
        [HttpPost]
        public IHttpActionResult setRegisterVehicle([FromBody]Register.cCrudRegisterVehicle prm)
        {
            var returnRes = Register.InsertRegisterVehicleProcess(prm);
            return Json(returnRes);
        }

        [HttpPost]
        [Route("api/Process/setTireScrap")]
        public IHttpActionResult setTireScrap([FromBody]Process.cCrudScrap prm)
        {
            var returnRes = Process.InsertScrapProcess(prm);
            return Json(returnRes);

        }


        [HttpPost]
        [Route("api/Process/sendToWorkshopBeforeScrap")]
        public IHttpActionResult sendToWorkshopBeforeScrap([FromBody]Process.cCrudSLWScrap prm)
        {
            var returnRes = Process.InsertSendToWorkshopBeforeScrapProcess(prm);
            return Json(returnRes);

        }


        [HttpPost]
        [Route("api/Process/setInbound")]
        public IHttpActionResult setInbound([FromBody]Process.cCrudInbound prm)
        {
            var returnRes = Process.InsertInbound(prm);
            return Json(returnRes);

        }

        //[HttpPost]
        //[Route("api/Process/setTireChecking")]
        //public IHttpActionResult setTireChecking([FromBody]Process.cCrudTyreChecking prm)
        //{
        //    var returnRes = Process.setTireChecking(prm);
        //    return Json(returnRes);

        //}


        [HttpPost]
        [Route("api/Process/setVehicleChange")]
        public IHttpActionResult setVehicleChange([FromBody]Process.cCrudVehicleChange prm)
        {
            var returnRes = Process.InsertVehicleChange(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Process/setVehicleChangeFromFds")]
        public IHttpActionResult setVehicleChangeFromFds([FromBody]Process.cCrudVehicleChange prm)
        {
            var returnRes = Process.InsertVehicleChangeFromFds(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Process/setTireChange")]
        public IHttpActionResult setTireChange([FromBody]Process.cCrudTireChange prm)
        {
            var returnRes = Process.InsertTireChange(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Process/setOutbound")]
        public IHttpActionResult setOutbound([FromBody]Process.cCrudOutbound prm)
        {
            var returnRes = Process.InsertOutbound(prm);
            return Json(returnRes);

        }


        [HttpPost]
        [Route("api/Process/setTireDisposal")]
        public IHttpActionResult setTireDisposal([FromBody]Process.cCrudDisposal prm)
        {
            var returnRes = Process.InsertDisposalProcess(prm);
            return Json(returnRes);

        }
        // PUT api/documentation
        /// <summary>
        /// Fungsi set Repair 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult setRepair([FromBody]Repair.cCrudRepair prm)
        {
            var returnRes = Repair.InsertRepairProcess(prm);
            return Json(returnRes);

        }

        // PUT api/documentation
        /// <summary>
        /// Fungsi set Inspection 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult setInspection([FromBody]Inspection.cCrudInsepection prm)
        {
            var returnRes = Inspection.InsertInspectionProcess(prm);
            return Json(returnRes);

        }

       

        // PUT api/documentation
        /// <summary>
        /// Fungsi set Outbound/Pickup 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult setPickup([FromBody]Process.cCrudPickup prm)
        {
            var returnRes = Process.InsertPickupProcess(prm);
            return Json(returnRes);

        }

        // PUT api/documentation
        /// <summary>
        /// Fungsi set Relocation
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult setRelocation([FromBody]Process.cCrudRelocation prm)
        {
            var returnRes = Process.InsertRelocationProcess(prm);
            return Json(returnRes);

        }

        // PUT api/documentation
        /// <summary>
        /// Fungsi set Inbound
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public IHttpActionResult setInbound([FromBody]Process.cCrudInbound prm)
        //{
        //    var returnRes = Process.InsertInboundProcess(prm);
        //    return Json(returnRes);

        //}

        // PUT api/documentation
        /// <summary>
        /// Fungsi set tire mileage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult updateTireMileage([FromBody]Process.tireMileage prm)
        {
            var returnRes = Process.InsertTireMileage(prm);
            return Json(returnRes);

        }
    }
}
