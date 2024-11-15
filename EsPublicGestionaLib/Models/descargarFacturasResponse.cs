﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EsPublicGestionaLib.DescargarFacturaResponse
{

    // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://webservice.face.gob.es")]
    
    public partial class descargarFacturaResponse
    {

        private @return returnField;

        /// <remarks/>
        [XmlElement("return")]
        public @return @return
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
    public partial class @return
    {

        private returnFactura facturaField;

        private returnResultado resultadoField;

        /// <remarks/>
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

        /// <remarks/>
        public returnResultado resultado
        {
            get
            {
                return this.resultadoField;
            }
            set
            {
                this.resultadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnFactura
    {

        private string mimeField;

        private string nombreField;

        private string facturaField;

        private string proveedorField;

        private string numeroField;

        private object serieField;

        private decimal importeField;

        private returnFacturaAnexoFile[] anexosField;

        /// <remarks/>
        public string mime
        {
            get
            {
                return this.mimeField;
            }
            set
            {
                this.mimeField = value;
            }
        }

        /// <remarks/>
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        public string factura
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
        public string proveedor
        {
            get
            {
                return this.proveedorField;
            }
            set
            {
                this.proveedorField = value;
            }
        }

        /// <remarks/>
        public string numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public object serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <remarks/>
        public decimal importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AnexoFile", IsNullable = false)]
        public returnFacturaAnexoFile[] anexos
        {
            get
            {
                return this.anexosField;
            }
            set
            {
                this.anexosField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnFacturaAnexoFile
    {

        private string anexoField;

        private string mimeField;

        private string nombreField;

        /// <remarks/>
        public string anexo
        {
            get
            {
                return this.anexoField;
            }
            set
            {
                this.anexoField = value;
            }
        }

        /// <remarks/>
        public string mime
        {
            get
            {
                return this.mimeField;
            }
            set
            {
                this.mimeField = value;
            }
        }

        /// <remarks/>
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnResultado
    {

        private byte codigoField;

        private string descripcionField;

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



}
