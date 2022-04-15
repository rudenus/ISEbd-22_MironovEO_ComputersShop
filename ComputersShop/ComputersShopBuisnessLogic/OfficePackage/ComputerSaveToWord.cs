using ComputersShopBuisnessLogic.OfficePackage.HelperEnums;
using ComputersShopBuisnessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopBuisnessLogic.OfficePackage
{
    public abstract class ComputerSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
                WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var computer in info.Computers)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { 
                        (computer.ComputerName + ": ", new WordTextProperties {
                        Size = "24",
                        Bold = true
                        }),
                        (Convert.ToInt32(computer.Price).ToString(), new WordTextProperties {
                        Size = "24"
                        })},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }
        public void CreateDocWareHouse(WordInfoWareHouse info)
        {
            CreateWord(new WordInfo()
            {
                FileName = info.FileName
            });
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
                WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateTable(new List<string>() { "Название", "ФИО ответственного", "Дата создания" });

            foreach (var wareHouse in info.WareHouses)
            {
                CreateRow(new WordRowParametrs()
                {
                    Texts = new List<string>()
                  {
                      wareHouse.WareHouseName,
                      wareHouse.ResponsiblePersonFIO,
                      wareHouse.DateCreate.ToShortDateString()
                  }
                });
            }
            SaveWord(new WordInfo()
            {
                FileName = info.FileName
            });
        }
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(WordRowParametrs wordRow);
        protected abstract void CreateWord(WordInfo info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void SaveWord(WordInfo info);
    }
}
