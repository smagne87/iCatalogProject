using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace iCatalogSite
{
    public class FileHelper
    {

        public static string GetFileName(string filename, bool withExtension = true)
        {
            if ((string.IsNullOrEmpty(filename)))
            {
                return string.Empty;
            }

            if ((withExtension))
            {
                return Path.GetFileName(filename);
            }
            else
            {
                return Path.GetFileName(filename).Replace(Path.GetExtension(filename), string.Empty);
            }
        }
        
        public static string SaveFile(string fileNamePrefix, HttpPostedFileBase file, string userFolder, string publicationFolder)
        {
            try
            {
                //get file's name and extension
                string fullname = FileHelper.GetFileName(file.FileName).Replace(" ", "_").Replace("+", "-");
                //set file's full name into repository
                string folder = userFolder;

                string relativeURL = string.Format("Media/{0}/{1}/{2}_{3}", folder, publicationFolder, fileNamePrefix, fullname);

                string localFullName = HttpContext.Current.Server.MapPath(string.Format("~/{0}", relativeURL));
                //save file to local folder
                if (!Directory.Exists(Path.GetDirectoryName(localFullName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localFullName));
                }

                file.SaveAs(localFullName);

                return relativeURL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteFile(string filePath)
        {
            if(!string.IsNullOrEmpty(filePath))
            {
                string fullPath = HttpContext.Current.Server.MapPath(filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }
}