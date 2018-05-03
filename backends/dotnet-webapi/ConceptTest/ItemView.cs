using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conceptTest
{
    /*
    - Any item type can have child items
    - The addition / editing / selecting and deletion (CRUD) of items happens through a central controller
    - This central controller checks the properties of the JSON provided and it must match the properties of the 
      destination object type being edited
    - Dynamic view classes e.g. with View attribute that extend the properties of the model class. 
        A factory then goes and automatically creates instances of these classes for extending the properties of the model class
    - The central controller also allows the filtering on the modal via dynamic query strings e.g. via AsQueryable()
        The filtering can either be done on an object or a view basis

    add(filename, item)
    edit(filename, item)
    del(filename, itemid)
    get(filename, itemid)
    list(filename, [itemtype], 
        objectfilter, viewfilter,
        objectorder, vieworder)
    listgrouped(filename, [itemtype], 
        objectfilter, viewfilter,
        objectorder, vieworder)

    - A test will need to be performed to check whether an object can be converted to JSON directly. If not, it is really not 
      a problem, because then a string can be returned and the frontend client would then be responsible for the conversion,
      but I doubt that this will be a necessary step
    */

    //public class ItemContainer
    //{
    //    public Item Item { get; set; }
    //    public ItemView View { get; set; }
    //    public List<ItemView> ChildItems { get; set; }
    //}
}
