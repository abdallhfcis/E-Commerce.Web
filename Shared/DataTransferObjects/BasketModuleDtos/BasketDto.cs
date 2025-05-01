using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.BasketModuleDtos
{
    public class BasketDto
    {
        public string Id { get; set; }//GUID:Created from client
        public ICollection<BasketItemsDto> Items { get; set; } = [];
    }
}
