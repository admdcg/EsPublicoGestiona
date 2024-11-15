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
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://webservice.face.gob.es")]
    public partial class solicitarNuevasFacturasResponse
    {
        
        private solicitarNuevasFacturasResponseReturn returnField;
        
        [XmlElement("return")]
        public solicitarNuevasFacturasResponseReturn @return
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
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class solicitarNuevasFacturasResponseReturn : ReturnResultado
    {
        private returnSolicitarNuevasFacturas[] facturasField;
        
        [System.Xml.Serialization.XmlArrayItemAttribute("solicitarNuevasFacturas")]
        public returnSolicitarNuevasFacturas[] facturas
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
    public partial class returnSolicitarNuevasFacturas
    {

        private string oficinaContableField;

        private string organoGestorField;

        private string unidadTramitadoraField;

        private string numeroRegistroField;

        private string fechaHoraRegistroField;

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

        /// <remarks/>
        public string organoGestor
        {
            get
            {
                return this.organoGestorField;
            }
            set
            {
                this.organoGestorField = value;
            }
        }

        /// <remarks/>
        public string unidadTramitadora
        {
            get
            {
                return this.unidadTramitadoraField;
            }
            set
            {
                this.unidadTramitadoraField = value;
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
        public string fechaHoraRegistro
        {
            get
            {
                return this.fechaHoraRegistroField;
            }
            set
            {
                this.fechaHoraRegistroField = value;
            }
        }
    }
}
