//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uch.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class Engineer
    {
        public int Employee_ID { get; set; }
        public string Specialization { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}