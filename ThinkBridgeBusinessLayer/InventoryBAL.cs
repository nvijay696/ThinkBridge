using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkBridgeDataLayer;
using ThinkBridgeModels;
using ThinkBridgeServiceLayer;

namespace ThinkBridgeBusinessLayer
{
    public class InventoryBAL
    {
        InventoryService inventoryService = new InventoryService();
        public async Task<bool> SaveInventory(InventoryDTO inventory)
        {
            return await inventoryService.SaveInventory(inventory);
        }
        public async Task<bool> UpdateInventory(InventoryDTO inventory)
        {
            return await inventoryService.UpdateInventory(inventory);
        }

        public async Task<List<Inventory>> GetInventoryList()
        {
            return await inventoryService.GetInventoryList();
        }

        public async Task<bool> SoftDeleteInventory(int id)
        {
            return await inventoryService.SoftDeleteInventory(id);
        }
        public async Task<bool> ForceDeleteInventory(int id)
        {
            return await inventoryService.SoftDeleteInventory(id);
        }
    }
}
