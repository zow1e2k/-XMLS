using System;

namespace Deanery
{
    public interface IDeanery
    {
        //Возвращает дату нужного формата для вставки в ссылку на справку
        abstract String GetDate();
        //Возвращает имя студента от его айди
        abstract String GetName(String id);
        //Ищет и устанавливает последний айди студента переменной last
        abstract void GetLastId();
        //Выводит в консоль краткую информацию о всех студентах института
        abstract void ShortInfoAboutAllStudents();
        //Создает нового студента
        abstract void CreateNewStudent();
        //Добавляет изучаемый предмет студенту
        abstract void AddSubject();
        //Добавляет изучаемый предмет всем студентам
        abstract void AddSubjectToAll();
        //Добавляет мед. справку студента в БД деканата
        abstract void AddMedCertificate();
        //Возвращает мед. справку студенту и "выбрасывает" её из БД деканата
        abstract void ReturnMedCertificate();
        //Удаляет студента из БД
        abstract void DeleteStudent();
        //Расформирование всей группы
        abstract void DeleteGroup();
        //Устанавливает рейтинг по предмету студенту
        abstract void SetRating();
        //Устанавливает экзаминационную оценку по предмету студенту
        abstract void SetExamMark();
    }
}
