using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsPublicGestionaLib.Models
{

    // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class consultarFacturaResponse
    {

        private consultarFacturaResponseReturn returnField;

        /// <remarks/>
        
        public consultarFacturaResponseReturn @return
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
    public partial class consultarFacturaResponseReturn : ReturnResultado
    {
        private returnFactura facturaField;
        public returnFactura factura
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
    public partial class returnFactura
    {

        private string numeroRegistroField;

        private returnFacturaTramitacion tramitacionField;

        private returnFacturaAnulacion anulacionField;

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
        public returnFacturaTramitacion tramitacion
        {
            get
            {
                return this.tramitacionField;
            }
            set
            {
                this.tramitacionField = value;
            }
        }

        /// <remarks/>
        public returnFacturaAnulacion anulacion
        {
            get
            {
                return this.anulacionField;
            }
            set
            {
                this.anulacionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnFacturaTramitacion
    {

        private ushort codigoField;

        private string descripcionField;

        private object motivoField;

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
        public string descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <remarks/>
        public object motivo
        {
            get
            {
                return this.motivoField;
            }
            set
            {
                this.motivoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnFacturaAnulacion
    {

        private ushort codigoField;

        private string descripcionField;

        private string motivoField;

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
        public string descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <remarks/>
        public string motivo
        {
            get
            {
                return this.motivoField;
            }
            set
            {
                this.motivoField = value;
            }
        }
    }
}
