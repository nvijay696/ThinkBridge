using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkBridgeDataLayer;
using ThinkBridgeModels;
using ThinkBridgeRepository.UnitOfWork;

namespace ThinkBridgeServiceLayer
{
    public class InventoryService
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<bool> SaveInventory(InventoryDTO inventory)
        {
            bool status;
            try
            {
                Inventory Inv = new Inventory();
                Inv.Id = inventory.Id;
                Inv.Name = inventory.Name;
                Inv.Description = inventory.Description;
                Inv.Price = inventory.Price;
                Inv.IsActive = true;
                Inv.CreatedOn = DateTime.Now;
                 _unitOfWork.InventoryRepository.Insert(Inv);
                await _unitOfWork.SaveAsync();                
                status = true;

            }
            catch (Exception)
            {
                throw;
            }
          
            return status;
        }


        public async Task<bool> UpdateInventory(InventoryDTO inventory)
        {
            bool status;
            try
            {
                var Inv = _unitOfWork.InventoryRepository.GetById(inventory.Id);
                Inv.Name = inventory.Name;
                Inv.Description = inventory.Description;
                Inv.Price = inventory.Price;
                Inv.IsActive = true;
                Inv.ModifiedOn = DateTime.Now;
                _unitOfWork.InventoryRepository.Update(Inv);
                await _unitOfWork.SaveAsync();
                status = true;

            }
            catch (Exception)
            {
                throw;
            }

            return status;
        }

        public async Task<List<Inventory>> GetInventoryList()
        {
            List<Inventory> list = new List<Inventory>();
            try
            {
              list = _unitOfWork.InventoryRepository.GetMany(i=>i.IsActive==true).ToList();
            }
            catch(Exception )
            {
                throw;
            }
            return await Task.FromResult(list);
        }

        public async Task<bool> SoftDeleteInventory(int id)
        {
            bool status;
            try
            {
                var data = _unitOfWork.InventoryRepository.Get(i => i.Id == id);
                data.IsActive = false;
                data.ModifiedOn = DateTime.Now;
                _unitOfWork.InventoryRepository.Update(data);
                await _unitOfWork.SaveAsync();
                status = true;

            }
            catch(Exception)
            {
                throw;
            }
            return status;
        }

        public async Task<bool> ForceDeleteInventory(int id)
        {
            bool status;
            try
            {
              
                _unitOfWork.InventoryRepository.Delete(id);
                await _unitOfWork.SaveAsync();
                status = true;

            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }



    }
}
