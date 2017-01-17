using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;
using System.IO;

namespace ACHE.Extensions
{
    public static class FileExtensions
    {
        public static void MoveWithReplace(this string sourceFileName, string destFileName)
        {

            //first, delete target file if exists, as File.Move() does not support overwrite
            if (File.Exists(destFileName))
                File.Delete(destFileName);
            
			File.Move(sourceFileName, destFileName);
        }
    }
}
