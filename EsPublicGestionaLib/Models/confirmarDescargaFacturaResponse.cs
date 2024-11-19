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
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://webservice.face.gob.es", IsNullable = false)]
    public class confirmarDescargaFacturaResponse
    {

        private confirmarDescargaFacturaResponseReturn returnField;

        /// <remarks/>
        [XmlElement("return")]
        public confirmarDescargaFacturaResponseReturn @return
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
    public class confirmarDescargaFacturaResponseReturn : ReturnResultado
    {
        private confirmarDescargaFacturaResponseReturnFactura facturaField;        
     
        public confirmarDescargaFacturaResponseReturnFactura factura
        {
            get
            {
                return this.facturaField;
            }
            set
            {
                this.facturaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class confirmarDescargaFacturaResponseReturnFactura
    {

        private ushort codigoField;

        private string numeroRegistroField;

        private string oficinaContableField;

        /// <remarks/>
        public ushort codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <remarks/>
        public string numeroRegistro
        {
            get
            {
                return this.numeroRegistroField;
            }
            set
            {
                this.numeroRegistroField = value;
            }
        }

        /// <remarks/>
        public string oficinaContable
        {
            get
            {
                return this.oficinaContableField;
            }
            set
            {
                this.oficinaContableField = value;
            }
        }
    }
}
