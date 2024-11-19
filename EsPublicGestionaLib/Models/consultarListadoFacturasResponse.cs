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
    public partial class consultarListadoFacturasResponse
    {

        private consultarListadoFacturasResponseReturn returnField;

        /// <remarks/>
        [XmlElement("return")]
        public consultarListadoFacturasResponseReturn @return
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
    public partial class consultarListadoFacturasResponseReturn : ReturnResultado
    {

        private returnConsultarListadoFacturas[] facturasField;
       
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("consultarListadoFacturas", IsNullable = false)]
        public returnConsultarListadoFacturas[] facturas
        {
            get
            {
                return this.facturasField;
            }
            set
            {
                this.facturasField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnConsultarListadoFacturas
    {

        private returnConsultarListadoFacturasFactura facturaField;

        private byte codigoField;

        private string descripcionField;

        /// <remarks/>
        public returnConsultarListadoFacturasFactura factura
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

        /// <remarks/>
        public byte codigo
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnConsultarListadoFacturasFactura
    {

        private string numeroRegistroField;

        private returnConsultarListadoFacturasFacturaTramitacion tramitacionField;

        private returnConsultarListadoFacturasFacturaAnulacion anulacionField;

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
        public returnConsultarListadoFacturasFacturaTramitacion tramitacion
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
        public returnConsultarListadoFacturasFacturaAnulacion anulacion
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
    public partial class returnConsultarListadoFacturasFacturaTramitacion
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
    public partial class returnConsultarListadoFacturasFacturaAnulacion
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
