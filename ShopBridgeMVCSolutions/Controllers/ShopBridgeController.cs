using System;
using System.Collections.Generic;
using System.Linq;////using System.Web;
using System.Web.Mvc;
using ShopBridgeEntities;
using ShopBridgeServiceProvider;
using System.Threading.Tasks;

namespace ShopBridgeMVCSolutions.Controllers
{
    public class ShopBridgeController : Controller
    {
        // GET: ShopBridge
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItemMaster()
        {
            ItemModel objItem = new ItemModel();
            if (Request.QueryString["id"] != null)
            {
                objItem = GetOneItem(Convert.ToInt32(Request.QueryString["id"]));
            }
            string a = Request.QueryString["id"];
                        
            return View(objItem);
        }
        
        public ActionResult SearchItem()
        {
            return View();
        }

        private ItemModel GetOneItem(int ItemId)
        {
            ItemsListModel objResponse = new ItemsListModel();
            ItemModel objModel = new ItemModel();
            objModel.ItemId = ItemId;
            objResponse = ShopBridgeProvider.GetItems(objModel);
            if (objResponse.IsValid)
            {
                foreach (ItemModel objItemModel in objResponse.ItemsList)
                {
                    objModel = objItemModel;
                }
            }
            return objModel;
        }
        public JsonResult GetSearchItems()
        {
            ItemsListModel objResponse = new ItemsListModel();
            SearchRequestModel objRequest = new SearchRequestModel();
            objRequest.CategoryId = 6;
            objRequest.UnitId = 6;
            objRequest.SearchText = "";
            objRequest.SortOrder = "itemModifiedDate Desc";
            objResponse = ShopBridgeProvider.SearchItems(objRequest);
            if (objResponse.IsValid)
                return Json(objResponse.ItemsList, JsonRequestBehavior.AllowGet);
            else
                return Json(new List<ItemModel>(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveItem(ItemModel objModel)
        {
            ShopBridgeResponseModel objResponseModel = new ShopBridgeResponseModel();
            objResponseModel = ShopBridgeProvider.SaveItem(objModel);
            
            return Json(new { success = objResponseModel.IsValid, message = objResponseModel.ResponseMessage }, JsonRequestBehavior.DenyGet);
           
        }

        [HttpPost]
        public ActionResult DeleteItem(int itemId)
        {
            ShopBridgeResponseModel objResponse = new ShopBridgeResponseModel();

            ItemModel objModel = new ItemModel();
            objModel.ItemId = Convert.ToInt32(itemId);
            objResponse = ShopBridgeProvider.DeleteItem(objModel);
            return Json(new { success = objResponse.IsValid, message = objResponse.ResponseMessage }, JsonRequestBehavior.DenyGet);
            

        }

    }
}