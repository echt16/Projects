//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Coursework
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeachersAccount
    {
        public long Id { get; set; }
        public int TeacherId { get; set; }
        public long LoginPasswordId { get; set; }
    
        public virtual LoginsPassword LoginsPassword { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
