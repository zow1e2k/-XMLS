using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace Deanery
{
    public class Deanery : IDeanery
    {
        public XmlDocument xDoc = new XmlDocument();
        private static int last;
        private static String clinicDoc = "http://www.sstu.ru/server/InstituteOfInformationTechnology/Deanery/Groups/2019/bachelor1-ist-21/student/clinicDocs/";

        public void GetLastId()
        {
            XmlElement xRoot = xDoc.DocumentElement;
            int lastId = 0;
            foreach (XmlNode xnode in xRoot)
                foreach (XmlNode childnode in xnode.ChildNodes)
                    foreach (XmlNode cn in childnode.ChildNodes)
                        if (lastId < int.Parse(cn.Attributes[0].Value.ToString())) lastId = int.Parse(cn.Attributes[0].Value.ToString());
            last = lastId;
        }

        public void ShortInfoAboutAllStudents()
        {
            XmlElement xRoot = xDoc.DocumentElement;
            bool a = false;
            foreach (XmlNode xnode in xRoot)
                foreach (XmlNode childnode in xnode.ChildNodes)
                    foreach (XmlNode cn in childnode.ChildNodes)
                    {
                        Console.WriteLine($"id: {cn.Attributes[0].Value.ToString()}");
                        foreach (XmlNode cnn in cn.ChildNodes)
                        {
                            if (cnn.Name == "FULLNAME") Console.WriteLine($"Full Name: {cnn.InnerText}");
                            if (cnn.Name == "BIRTHDAY")
                            {
                                Console.WriteLine($"Date of Birth: {cnn.InnerText}");
                                Console.WriteLine($"Group: {childnode.Attributes[0].Value.ToString()}");
                                a = true;
                            }
                            if (!a) continue;
                            if (cnn.Name == "GRADES")
                            {
                                if (cnn.ChildNodes.Count != 0)
                                {
                                    foreach (XmlNode t in cnn.ChildNodes)
                                    {
                                        if (t.Name == "SUBJECT")
                                        {
                                            Console.WriteLine($"--------------------\nSubject: {t.Attributes[0].Value.ToString()}");
                                            foreach (XmlNode l in t.ChildNodes)
                                            {
                                                if (l.Name == "RATING") Console.WriteLine("Rating: " + l.InnerText);
                                                if (l.Name == "CLASSIFICATION") Console.WriteLine("Classification: " + l.InnerText);
                                                if (l.Name == "EXAM") Console.WriteLine("Exam: " + l.InnerText);
                                            }
                                        }
                                    }
                                }
                            }
                            if (cnn.Name == "PAYMENT")
                            {
                                Console.WriteLine($"--------------------\nPayment state: {cnn.Attributes[0].Value.ToString()}");
                                foreach (XmlNode m in cnn.ChildNodes)
                                {
                                    if (m.Name == "NUMBEROFDOCUMENT") Console.WriteLine("Number of document: " + m.InnerText);
                                    if (m.Name == "LINKTOOBJECT") Console.WriteLine("Link to object: " + m.InnerText);
                                }
                            }
                            if (cnn.Name == "REFERENCE" && cnn.Attributes[0].Value.ToString().Equals("EDUCATION"))
                            {
                                Console.WriteLine($"--------------------\nEducation document state: {cnn.Attributes[1].Value.ToString()}");
                                foreach (XmlNode v in cnn.ChildNodes)
                                {
                                    if (v.Name == "NUMBEROFREFDOCUMENT") Console.WriteLine("Number of document: " + v.InnerText);
                                    if (v.Name == "LINKTOREFOBJECT") Console.WriteLine("Link to object: " + v.InnerText);
                                }
                            }
                            if (cnn.Name == "REFERENCE" && cnn.Attributes[0].Value.ToString().Equals("MEDCERTIFICATE"))
                            {
                                Console.WriteLine($"--------------------\nMed document state: {cnn.Attributes[1].Value.ToString()}");
                                foreach (XmlNode y in cnn.ChildNodes)
                                {
                                    if (y.Name == "NUMBEROFREFDOCUMENT") Console.WriteLine("Number of document: " + y.InnerText);
                                    if (y.Name == "LINKTOREFOBJECT") Console.WriteLine("Link to object: " + y.InnerText);
                                }
                            }
                        }
                        Console.WriteLine();
                    }
            Console.WriteLine();
        }

        public bool validGroup(String str)
        {
            if (str.Length < 5 || str.Length > 50)
            {
                Console.WriteLine("Group must have under 50 and above 5 characters");
                return false;
            }
            return true;
        }

        public bool validName(String str)
        {
            if (str.Length < 6 || str.Length > 99)
            {
                Console.WriteLine("Name must have under 100 and above 6 characters");
                return false;
            }
            return true;
        }

        public bool validDate(String str)
        {
            Regex regex = new Regex(@"\d{2}.\d{2}.\d{4}");
            if (str.Length < 6 || str.Length > 10)
            {
                Console.WriteLine("Date must have under 11 and above 6 characters");
                return false;
            }
            if (!regex.IsMatch(str))
            {
                Console.WriteLine("The right format is -> ##.##.####");
                return false;
            }
            return true;
        }

        public bool validStudentID(String str)
        {
            Regex regex = new Regex(@"\d{6}");
            if (str.Length < 6 || str.Length > 6)
            {
                Console.WriteLine("Student ID must have 6 characters");
                return false;
            }
            if (!regex.IsMatch(str))
            {
                Console.WriteLine("Student ID must be digit's array");
                return false;
            }
            return true;
        }

        public bool validSubjectName(String str)
        {
            if (str.Length < 1 || str.Length > 50)
            {
                Console.WriteLine("Subject ID must have under 50 and above 1 characters");
                return false;
            }
            return true;
        }

        public bool validNumberOfDocument(String str)
        {
            if (str.Length < 10 || str.Length > 100)
            {
                Console.WriteLine("Number of document must have under 100 and above 10 characters");
                return false;
            }
            return true;
        }

        public bool validRating(String str)
        {
            Regex regex = new Regex(@"\d+.\d+");
            if (!regex.IsMatch(str))
            {
                Console.WriteLine("Rating must be float");
                return false;
            }
            return true;
        }

        public bool validClassification(String str)
        {
            if (str.ToUpper().Equals("TRUE") || str.ToUpper().Equals("FALSE")) return true;
            Console.WriteLine("Classification must be true or false");
            return false;
        }

        public bool validExamMark(String str)
        {
            Regex regex = new Regex(@"\d{1}");
            if (int.Parse(str) > 5 || int.Parse(str) < 2)
            {
                Console.WriteLine("Exam mark must be from 2 to 5");
                return false;
            }
            if (!regex.IsMatch(str))
            {
                Console.WriteLine("Exam mark must be 1 digit");
                return false;
            }
            return true;
        }

        public void CreateNewStudent()
        {
            GetLastId();
            Console.Write("Input Group: ");
            String group = Console.ReadLine();
            while (!validGroup(group)) group = Console.ReadLine();
            Console.Write("Input Full Name: ");
            String fullName = Console.ReadLine();
            while (!validName(fullName)) fullName = Console.ReadLine();
            Console.Write("Input Date of Birth: ");
            String dateOfBirth = Console.ReadLine();
            while (!validDate(dateOfBirth)) dateOfBirth = Console.ReadLine();
            Console.Write("Input Subject Name: ");
            String name = Console.ReadLine();
            while (!validSubjectName(name)) name = Console.ReadLine();
            Console.Write("Input Number of Payment Document: ");
            String numberText = Console.ReadLine();
            Console.Write("Input Link to Object: ");
            String linkText = Console.ReadLine();
            Console.Write("Input Number of Education Document: ");
            String numberRefText = Console.ReadLine();
            Console.Write("Input Link to Object: ");
            String linkRefText = Console.ReadLine();

            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement studentElem = xDoc.CreateElement("STUDENT");
            XmlElement fullNameElem = xDoc.CreateElement("FULLNAME");
            XmlElement dateOfBirthElem = xDoc.CreateElement("BIRTHDAY");
            XmlElement gradesElem = xDoc.CreateElement("GRADES");
            XmlElement subjectElem = xDoc.CreateElement("SUBJECT");
            XmlElement ratingElem = xDoc.CreateElement("RATING");
            XmlElement classificationElem = xDoc.CreateElement("CLASSIFICATION");
            XmlElement examElem = xDoc.CreateElement("EXAM");
            XmlElement paymentElem = xDoc.CreateElement("PAYMENT");
            XmlElement numberElem = xDoc.CreateElement("NUMBEROFDOCUMENT");
            XmlElement linkElem = xDoc.CreateElement("LINKTOOBJECT");
            XmlElement refElem = xDoc.CreateElement("REFERENCE");
            XmlElement linkRefElem = xDoc.CreateElement("LINKTOREFOBJECT");
            XmlElement numberRefElem = xDoc.CreateElement("NUMBEROFREFDOCUMENT");
            XmlAttribute id = xDoc.CreateAttribute("id");

            XmlText xFullName = xDoc.CreateTextNode(fullName);
            XmlText xDateOfBirth = xDoc.CreateTextNode(dateOfBirth);
            id.Value = (last+1).ToString();
            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    if (cn.Attributes[0].Value.ToString().Equals(group))
                    {
                        studentElem.AppendChild(fullNameElem);
                        studentElem.AppendChild(dateOfBirthElem);
                        studentElem.SetAttribute("id", id.Value.ToString());
                        studentElem.SetAttribute("xmlns", "E:/projects/XML Labs IST-2/RGR");
                        subjectElem.SetAttribute("name", name);
                        ratingElem.InnerText = "0.0";
                        subjectElem.AppendChild(ratingElem);
                        classificationElem.InnerText = "false";
                        subjectElem.AppendChild(classificationElem);
                        examElem.InnerText = "0";
                        subjectElem.AppendChild(examElem);
                        gradesElem.AppendChild(subjectElem);
                        studentElem.AppendChild(gradesElem);
                        paymentElem.SetAttribute("state", "NOTPAID");
                        numberElem.InnerText = numberText;
                        paymentElem.AppendChild(numberElem);
                        linkElem.InnerText = linkText;
                        paymentElem.AppendChild(linkElem);
                        studentElem.AppendChild(paymentElem);

                        refElem.SetAttribute("type", "EDUCATION");
                        refElem.SetAttribute("state", "ISSUED");
                        numberRefElem.InnerText = numberRefText;
                        refElem.AppendChild(numberRefElem);
                        linkRefElem.InnerText = linkRefText;
                        refElem.AppendChild(linkRefElem);
                        studentElem.AppendChild(refElem);
                        fullNameElem.AppendChild(xFullName);
                        dateOfBirthElem.AppendChild(xDateOfBirth);
                        cn.AppendChild(studentElem);
                    }
            Console.WriteLine("Student sucessfully added");
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }

        public void AddSubject()
        {
            bool a = false;
            Console.Write("Input Student ID: ");
            String studentID = Console.ReadLine();
            while (!validStudentID(studentID)) studentID = Console.ReadLine();
            Console.Write("Input Name of Subject: ");
            String name = Console.ReadLine();
            while (!validSubjectName(name)) name = Console.ReadLine();
            Console.Write("Input Rating: ");
            String rating = Console.ReadLine();
            while (!validRating(rating)) rating = Console.ReadLine();
            Console.Write("Input Classification (TRUE/FALSE): ");
            String classification = Console.ReadLine();
            while (!validClassification(classification)) classification = Console.ReadLine();
            Console.Write("Input Mark of Exam: ");
            String exam = Console.ReadLine();
            while (!validExamMark(exam)) exam = Console.ReadLine();

            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement xSubject = xDoc.CreateElement("SUBJECT");
            XmlElement xRating = xDoc.CreateElement("RATING");
            XmlElement xClassification = xDoc.CreateElement("CLASSIFICATION");
            XmlElement xExam = xDoc.CreateElement("EXAM");

            XmlText TextRating = xDoc.CreateTextNode(rating);
            XmlText TextClassification = xDoc.CreateTextNode(classification);
            XmlText TextExam = xDoc.CreateTextNode(exam);

            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    foreach (XmlNode c in cn.ChildNodes)
                    {
                        if (c.Attributes[0].Value.ToString().Equals(studentID)) a = true;
                        foreach (XmlNode b in c.ChildNodes)
                            if ((b.Name == "GRADES") && a)
                            {
                                xSubject.AppendChild(xRating);
                                xSubject.AppendChild(xClassification);
                                xSubject.AppendChild(xExam);
                                xSubject.SetAttribute("name", name);
                                xSubject.SetAttribute("xmlns", "E:/projects/XML Labs IST-2/RGR");
                                xRating.AppendChild(TextRating);
                                xClassification.AppendChild(TextClassification);
                                xExam.AppendChild(TextExam);
                                b.AppendChild(xSubject);
                                Console.WriteLine("subject sucessfully added");
                                a = false;
                                break;
                            }
                    }
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }

        public void AddSubjectToAll()
        {
            Console.Write("Input Name of Subject: ");
            String name = Console.ReadLine();
            while (!validSubjectName(name)) name = Console.ReadLine();
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    foreach (XmlNode c in cn.ChildNodes)
                        foreach (XmlNode b in c.ChildNodes)
                            if (b.Name == "GRADES")
                            {
                                XmlElement xSubject = xDoc.CreateElement("SUBJECT");
                                XmlElement xRating = xDoc.CreateElement("RATING");
                                XmlElement xClassification = xDoc.CreateElement("CLASSIFICATION");
                                XmlElement xExam = xDoc.CreateElement("EXAM");
                                XmlText TextRating = xDoc.CreateTextNode("0.0");
                                XmlText TextClassification = xDoc.CreateTextNode("FALSE");
                                XmlText TextExam = xDoc.CreateTextNode("0");
                                xSubject.AppendChild(xRating);
                                xSubject.AppendChild(xClassification);
                                xSubject.AppendChild(xExam);
                                xSubject.SetAttribute("name", name);
                                xSubject.SetAttribute("xmlns", "E:/projects/XML Labs IST-2/RGR");
                                xRating.AppendChild(TextRating);
                                xClassification.AppendChild(TextClassification);
                                xExam.AppendChild(TextExam);
                                b.AppendChild(xSubject);
                            }
            Console.WriteLine("subject sucessfully added to all");
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }

        public String GetName(String id)
        {
            bool a = false;
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    foreach (XmlNode c in cn.ChildNodes)
                    {
                        if (c.Attributes[0].Value.ToString().Equals(id)) a = true;
                        foreach (XmlNode b in c.ChildNodes)
                            if ((b.Name == "FULLNAME") && a) return b.InnerText;
                    }
            return id;
        }

        public String GetDate()
        {
            return DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year;
        }

        public void AddMedCertificate()
        {
            bool a = false;
            Console.Write("Input Student ID: ");
            String studentID = Console.ReadLine();
            while (!validStudentID(studentID)) studentID = Console.ReadLine();
            Console.Write("Input Number of Document: ");
            String number = Console.ReadLine();
            while (!validNumberOfDocument(number)) number = Console.ReadLine();

            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement xReference = xDoc.CreateElement("REFERENCE");
            XmlElement xNumber = xDoc.CreateElement("NUMBEROFREFDOCUMENT");
            XmlElement xLink = xDoc.CreateElement("LINKTOREFOBJECT");
            XmlText TextNumber = xDoc.CreateTextNode(number);
            XmlText TextLink = xDoc.CreateTextNode(clinicDoc + GetDate() + "_" + GetName(studentID).Replace(' ', '_') + ".pdf");

            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    foreach (XmlNode c in cn.ChildNodes)
                    {
                        if (c.Attributes[0].Value.ToString().Equals(studentID))
                        {
                            Console.WriteLine(studentID);
                            a = true;
                        }
                        foreach (XmlNode b in c.ChildNodes)
                            if ((b.Name == "PAYMENT") && a)
                            {
                                xReference.AppendChild(xNumber);
                                xReference.AppendChild(xLink);
                                xReference.SetAttribute("type", "MEDCERTIFICATE");
                                xReference.SetAttribute("state", "RECEIVED");
                                xReference.SetAttribute("xmlns", "E:/projects/XML Labs IST-2/RGR");
                                xNumber.AppendChild(TextNumber);
                                xLink.AppendChild(TextLink);
                                c.AppendChild(xReference);
                                Console.WriteLine("Medcertificate sucessfully added");
                                a = false;
                                break;
                            }
                    }
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }

        public void DeleteStudent()
        {
            Console.Write("Input Student ID: ");
            String studentID = Console.ReadLine();
            while (!validStudentID(studentID)) studentID = Console.ReadLine();
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
                foreach (XmlNode childnode in xnode.ChildNodes)
                    foreach (XmlNode cn in childnode.ChildNodes)
                        if (cn.Attributes[0].Value.ToString().Equals(studentID)) childnode.RemoveChild(cn);
            Console.WriteLine($"Student {GetName(studentID)} [id: {studentID}] sucessfully deleted");
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }

        public void DeleteGroup()
        {
            Console.Write("Input Group Name: ");
            String group = Console.ReadLine();
            while (!validGroup(group)) group = Console.ReadLine();
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
                foreach (XmlNode childnode in xnode.ChildNodes)
                    if (childnode.Attributes[0].Value.ToString().Equals(group)) xnode.RemoveChild(childnode);
            Console.WriteLine($"Group {group} sucessfully deleted");
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }
        public void ReturnMedCertificate()
        {
            bool a = false;
            Console.Write("Input Student ID: ");
            String studentID = Console.ReadLine();
            while (!validStudentID(studentID)) studentID = Console.ReadLine();
            Console.Write("Input Number of Document: ");
            String number = Console.ReadLine();
            while (!validNumberOfDocument(number)) number = Console.ReadLine();
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    foreach (XmlNode c in cn.ChildNodes)
                    {
                        if (c.Attributes[0].Value.ToString().Equals(studentID)) a = true;
                        foreach (XmlNode b in c.ChildNodes)
                            if ((b.Name == "REFERENCE") && a)
                                foreach (XmlNode n in b.ChildNodes)
                                    if (n.Name == "NUMBEROFDOCUMENT" && n.InnerText.Equals(number))
                                    {
                                        if (b.Attributes[1].Value.ToString().Equals("ISSUED"))
                                        {
                                            Console.WriteLine($"Medcertificate {number} issued yet");
                                            break;
                                        }
                                        b.Attributes[1].Value = "ISSUED";
                                        Console.WriteLine($"Medcertificate {number} sucessfully issued");
                                        a = false;
                                        break;
                                    }
                    }
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }

        public void SetRating()
        {
            Console.Write("Input Student ID: ");
            String studentID = Console.ReadLine();
            while (!validStudentID(studentID)) studentID = Console.ReadLine();
            Console.Write("Input Name of Subject: ");
            String name = Console.ReadLine();
            while (!validSubjectName(name)) name = Console.ReadLine();
            Console.Write("Input Rating: ");
            String rating = Console.ReadLine();
            while (!validRating(rating)) rating = Console.ReadLine();
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    foreach (XmlNode c in cn.ChildNodes)
                        if (c.Attributes[0].Value.ToString().Equals(studentID))
                            foreach (XmlNode b in c.ChildNodes)
                                if (b.Name == "GRADES")
                                    foreach (XmlNode m in b.ChildNodes)
                                        if (m.Attributes[0].Value.ToString().Equals(name.ToUpper()))
                                            foreach (XmlNode l in m.ChildNodes)
                                                if (l.Name == "RATING") { l.InnerText = rating; break; }
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }

        public void SetExamMark()
        {
            Console.Write("Input Student ID: ");
            String studentID = Console.ReadLine();
            while (!validStudentID(studentID)) studentID = Console.ReadLine();
            Console.Write("Input Name of Subject: ");
            String name = Console.ReadLine();
            while (!validSubjectName(name)) name = Console.ReadLine();
            Console.Write("Input Exam Mark: ");
            String mark = Console.ReadLine();
            while (!validExamMark(mark)) mark = Console.ReadLine();

            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode childNode in xRoot)
                foreach (XmlNode cn in childNode.ChildNodes)
                    foreach (XmlNode c in cn.ChildNodes)
                        if (c.Attributes[0].Value.ToString().Equals(studentID))
                            foreach (XmlNode b in c.ChildNodes)
                                if (b.Name == "GRADES")
                                    foreach (XmlNode m in b.ChildNodes)
                                        if (m.Attributes[0].Value.ToString().Equals(name.ToUpper()))
                                            foreach (XmlNode l in m.ChildNodes)
                                                if (l.Name == "EXAM") { l.InnerText = mark; break; }
            xDoc.Save($"{MainApp.path}/Deanery.xml");
        }
    }
}
