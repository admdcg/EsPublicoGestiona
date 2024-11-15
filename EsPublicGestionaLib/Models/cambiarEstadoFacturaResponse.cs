using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EsPublicGestionaLib.Models
{

    // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]    
    public partial class cambiarEstadoFacturaResponse
    {
        
        private cambiarEstadoFacturaResponseReturn returnField;

        /// <remarks/>
        [XmlElement("return")]
        public cambiarEstadoFacturaResponseReturn @return
        {
            get
            {
                return this.returnField;
            }
            set
            {
                this.returnField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]    
    public partial class cambiarEstadoFacturaResponseReturn : ReturnResultado
    {

    }
}
