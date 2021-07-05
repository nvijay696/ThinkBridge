using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ThinkBridgeBusinessLayer;
using ThinkBridgeModels;

namespace ThinkBridge.Controllers
{
    //[Authorize]

    [RoutePrefix("Inventory")]
    public class InventoryController : ApiController
    {
        readonly InventoryBAL inventoryBAL = new InventoryBAL();

        [HttpPost]
        [Route("SaveInventory")]
        public async Task<HttpResponseMessage> SaveInventory([FromBody] InventoryDTO inventory)
        {
            ApiResponse response = new ApiResponse();
            bool result;
            try
            {
                if (inventory != null)
                {
                    result = await inventoryBAL.SaveInventory(inventory);
                    if (result == true)
                    {
                        response.message = "Data Saved Successfully";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Some Error Occured";
                        response.statusCode = HttpStatusCode.InternalServerError;

                    }
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message.ToString();
                response.status = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                Logger.log.Error(ex.ToString());

            }
            return Request.CreateResponse(response.statusCode, response);
        }

        [HttpPost]
        [Route("UpdateInventory")]
        public async Task<HttpResponseMessage> UpdateInventory([FromBody] InventoryDTO inventory)
        {
            ApiResponse response = new ApiResponse();
            bool result;
            try
            {
                if (inventory != null)
                {
                    result = await inventoryBAL.UpdateInventory(inventory);
                    if (result == true)
                    {
                        response.message = "Data Updated Successfully";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Some Error Occured";
                        response.statusCode = HttpStatusCode.InternalServerError;

                    }
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message.ToString();
                response.status = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                Logger.log.Error(ex.ToString());

            }
            return Request.CreateResponse(response.statusCode, response);
        }

        [HttpGet]
        [Route("GetInventoryList")]
        public async Task<HttpResponseMessage> GetInventoryList()
        {
            ApiResponse response = new ApiResponse();
            try
            {                
                var result = await inventoryBAL.GetInventoryList();
                if(result != null)
                {
                    response.message = "Data Retrived Successfully";
                    response.data = result;
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.message = "Some Error Occured";
                    response.statusCode = HttpStatusCode.InternalServerError;
                }
               
            }
            catch(Exception ex)
            {
                response.message = ex.Message.ToString();
                response.status = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                Logger.log.Error(ex.ToString());

            }
            return Request.CreateResponse(response.statusCode, response);

        }

        [HttpGet]
        [Route("DeleteInventory")]
        public async Task<HttpResponseMessage> DeleteInventory(int id)
        {
            ApiResponse response = new ApiResponse();
            bool result;
            try
            {
               
                    result = await inventoryBAL.SoftDeleteInventory(id);
                    if (result == true)
                    {
                        response.message = "Deleted Successfully";
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.message = "Some Error Occured";
                        response.statusCode = HttpStatusCode.InternalServerError;

                    }
                
            }
            catch(Exception ex)
            {
                response.message = ex.Message.ToString();
                response.status = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                Logger.log.Error(ex.ToString());
            }
            return Request.CreateResponse(response.statusCode, response);


        }
    

    }
}
