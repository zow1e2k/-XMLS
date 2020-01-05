using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Deanery
{
    public class MainApp
    {
        public static String path = "E:/projects/XML Labs IST-2/RGR";
        public static void Main(string[] args)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(path, $"{path}/DEANERYOFFICE.xsd");
                settings.ValidationType = ValidationType.Schema;
                XmlReader reader = XmlReader.Create($"{path}/Deanery.xml", settings);
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                XmlDocument document = new XmlDocument();
                document.Load(reader);
                document.Validate(eventHandler);
                reader.Close();
                Deanery deanery = new Deanery();
                deanery.xDoc.Load($"{path}/Deanery.xml");
                deanery.GetLastId();
                while (true)
                {
                    Console.Clear();
                    Console.Write("\t\tIT Faculty Deanery (Insert from 1 to 10):\n" +
                                    " \n1. Short Info About All Students\t2. Create New Student\n3. Set Subject Rating\t\t\t4. Set Exam Mark" +
                                    "\n5. Add Subject\t\t\t\t6. Add Subject to All\n7. Add Med Certificate\t\t\t8. Return Med Certificate" +
                                    "\n9. Delete Student\t\t\t10. Delete Group\n\n>>\t");
                    String num = Console.ReadLine();
                    switch (num)
                    {
                        case "1":
                            Console.Clear();
                            deanery.ShortInfoAboutAllStudents();
                            break;
                        case "2":
                            Console.Clear();
                            deanery.CreateNewStudent();
                            break;
                        case "3":
                            Console.Clear();
                            deanery.SetRating();
                            break;
                        case "4":
                            Console.Clear();
                            deanery.SetExamMark();
                            break;
                        case "5":
                            Console.Clear();
                            deanery.AddSubject();
                            break;
                        case "6":
                            Console.Clear();
                            deanery.AddSubjectToAll();
                            break;
                        case "7":
                            Console.Clear();
                            deanery.AddMedCertificate();
                            break;
                        case "8":
                            Console.Clear();
                            deanery.ReturnMedCertificate();
                            break;
                        case "9":
                            Console.Clear();
                            deanery.DeleteStudent();
                            break;
                        case "10":
                            Console.Clear();
                            deanery.DeleteGroup();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Input from 1 to 10");
                            break;
                    }
                    Console.WriteLine("If you want stop press space, else press any button");
                    int key = Console.ReadKey().KeyChar;
                    if (key == 32) break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
                default: Console.WriteLine("Success"); break;
            }
        }
    }
}

