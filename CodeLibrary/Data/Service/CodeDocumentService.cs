﻿using CodeLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLibrary.Data.Service
{
    public class CodeDocumentService
    {
        public static async Task<int> InsertCodeDocument(CodeDocument codeDocument)
        {
            codeDocument.CreatedUtc = DateTime.UtcNow;
            codeDocument.LastUpdatedUtc = DateTime.UtcNow;

            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var insertTask = entityRepository.InsertAsync(codeDocument);
            var result = await insertTask;
            return result.Id;
        }

        public static async Task<int> UpdateCodeDocument(CodeDocument codeDocument)
        {
            codeDocument.LastUpdatedUtc = DateTime.UtcNow;

            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var updateTask = entityRepository.UpdateAsync(codeDocument);
            var result = await updateTask;
            return result;
        }

        public static async Task<CodeDocument> GetCodeDocumentByTitle(string title)
        {
            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var selectTask = entityRepository.Where(t => t.Title == title).FirstAsync();
            var result = await selectTask;
            return result;
        }
        public static async Task<CodeDocument> GetCodeDocumentById(int id)
        {
            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var selectTask = entityRepository.Where(t => t.Id == id).FirstAsync();
            var result = await selectTask;
            return result;
        }

        public static async Task<List<CodeDocument>> GetCodeDocumentTitlesByTitle(string titleName)
        {
            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            if (string.IsNullOrWhiteSpace(titleName))
            {
                var selectTask = entityRepository.Where(t => t.Deleted == false).OrderBy(t => t.ProgrammingLanguageId).ToListAsync();
                var list = await selectTask;
                var result = list;
                return result;
            }
            else
            {
                var selectTask = entityRepository.Where(t => t.Title.Contains(titleName) && t.Deleted == false).OrderBy(t => t.ProgrammingLanguageId).ToListAsync();
                var list = await selectTask;
                var result = list;
                return result;
            }
        }

        public static async Task<int> DeleteCodeDocument(CodeDocument codeDocument)
        {
            codeDocument.Deleted = true;
            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var deleteTask = entityRepository.UpdateAsync(codeDocument);
            var result = await deleteTask;
            return result;
        }

        public static async Task<List<string>> GetCodeDocumentTitlesByKeyWord(string keyWord)
        {
            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var selectTask = entityRepository.Where(t => t.KeyWords.Contains(keyWord)).ToListAsync();
            var list = await selectTask;
            var result = list.Select(t => t.Title).ToList();
            return result;
        }

        public static async Task<List<string>> GetCodeDocumentTitleByContent(string s)
        {
            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var selectTask = entityRepository.Where(t => t.Datas.Contains(s)).ToListAsync();
            var list = await selectTask;
            var result = list.Select(t => t.Title).ToList();
            return result;
        }

        public static async Task<long> GetCodeDocumentCount()
        {
            var entityRepository = DataHelper.Instance.Current.GetRepository<CodeDocument>();
            var selectTask = entityRepository.Where(t => t.Deleted == false).CountAsync();
            var result = await selectTask;
            return result;
        }
    }
}
