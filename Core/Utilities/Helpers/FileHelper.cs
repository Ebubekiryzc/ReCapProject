using System;
using System.IO;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {
        public static IDataResult<string> AddAsync(IFormFile file, string path)
        {
            string fileNameWithGUID;
            try
            {
                if (file == null) throw new FileNotFoundException();

                fileNameWithGUID = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream fileStream = new FileStream(Path.Combine(path, fileNameWithGUID), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            catch (Exception e)
            {
                return new ErrorDataResult<string>(message: e.Message);
            }

            return new SuccessDataResult<string>(Path.Combine(path, fileNameWithGUID), "Operation Successful!");
        }

        public static IResult DeleteAsync(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException();
                }
                File.Delete(path);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

            return new SuccessResult("Operation Successful!");
        }

    }
}