using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace EsPublicGestionaLib.Helpers
{
    public static class ExceptionHelper
    {
        public static string MountMessageException(Exception ex)
        {
            if (ex == null)
            {
                return String.Empty;
            }
            else
            {
                var str = MountMessageException(ex.InnerException);
                if (!String.IsNullOrEmpty(str))
                {
                    str = " - " + str;
                }
                return ex.Message + str;
            }
        }

        

        public static String MountStackTraceException(Exception ex)
        {
            if (ex == null)
            {
                return String.Empty;
            }
            else
            {
                var str = MountStackTraceException(ex.InnerException);
                if (!String.IsNullOrEmpty(str))
                {
                    str = " - " + str;
                }                
                return ex.StackTrace + str;
            }
        }

        public static String MountTypeException(Exception ex)
        {
            if (ex == null)
            {
                return String.Empty;
            }
            else
            {
                var str = MountTypeException(ex.InnerException);
                if (!String.IsNullOrEmpty(str))
                {
                    str = " - " + str;
                }
                return ex.GetType().ToString() + str;
            }
        }

        public static String MountSourceException(Exception ex)
        {
            if (ex == null)
            {
                return String.Empty;
            }
            else
            {
                var str = MountSourceException(ex.InnerException);
                if (!String.IsNullOrEmpty(str))
                {
                    str = " - " + str;
                }
                return ex.Source + str;
            }
        }
       
        public static String MountInnerException(Exception ex)
        {
            return MountException(ex.InnerException);
        }

        public static String MountException(Exception ex)
        {
            return $"Type: {ExceptionHelper.MountTypeException(ex.InnerException)} / Message: {ExceptionHelper.MountMessageException(ex)} / StackTrace: {ExceptionHelper.MountStackTraceException(ex.InnerException)}";
        }
    }
}
