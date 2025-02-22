using System;
using System.Linq;
using System.Collections.Generic;
using LinqGroupByMethod;
using Microsoft.Identity.Client;
using System.ComponentModel.Design;  // Ensure namespace matches

namespace Linqgroupbymethod
{
    public class Grpbymeth
    {
        public static void DisplayGroupedData()
        {

            //GROUP BY METHOD//



            // SELECT METHOD//
            using (var context = new StudentDbcontext())
            {
                var groupedMarks = context.Record
                    .GroupBy(record => record.StudentID)
                    .Select(group => new
                    {
                        StudentID = group.Key,
                        Marks = group.Select(record => new
                        {
                            RecordID = record.RecordID,
                            Tamil = record.Tamil,
                            English = record.English,
                            Maths = record.Maths,
                            Science = record.Science,
                            Social = record.Social
                        }).ToList()
                    })
                    .ToList();

                Console.WriteLine("Grouped Marks Information:");
                foreach (var studentMarks in groupedMarks)
                {
                    Console.WriteLine($"Student ID: {studentMarks.StudentID}");
                    Console.WriteLine("Marks:");
                    foreach (var marks in studentMarks.Marks)
                    {
                        Console.WriteLine($"Record ID: {marks.RecordID}, Tamil: {marks.Tamil}, English: {marks.English}, Maths: {marks.Maths}, Science: {marks.Science}, Social: {marks.Social}");
                    }
                    Console.WriteLine("---------------------------------------------------");
                }
            }
        }

        // ORDER BY METHOD //
        public static void orderby()
        {
            using (var context = new StudentDbcontext())
            {

                var ascendingOrder = context.stuinformation
                    .OrderBy(student => student.Age)
                    .ToList();

                Console.WriteLine("\nStudents sorted in Ascending Order By Age:");
                foreach (var student in ascendingOrder)
                {
                    Console.WriteLine($"Student ID: {student.StudentID}, Name: {student.FirstName} {student.LastName} Age:{student.Age}");
                }

                Console.WriteLine("---------------------------------------------------");


                var descendingOrder = context.stuinformation
                    .OrderByDescending(student => student.Age)
                    .ToList();

                Console.WriteLine("\nStudents sorted in Descending Order By Age:");
                foreach (var student in descendingOrder)
                {
                    Console.WriteLine($"Student ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Age:{student.Age} ");
                }
            }

        }
        //OFTYPE METHOD//
        public static void FilterUsingOfType()
        {
            using (var context = new StudentDbcontext())
            {
                var allStudents = context.stuinformation.ToList();

                var filteredStudents = allStudents.OfType<StudentInformation>().Where(s => s.Age > 18).ToList();

                Console.WriteLine("\nStudents filtered using OfType (Age > 18):");
                foreach (var student in filteredStudents)
                {
                    Console.WriteLine($"Student ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}");
                }
            }
        }

        //GROUP JOIN METHOD//
        public static void GroupJoinMethod()
        {
            using (var context = new StudentDbcontext())
            {
                var groupJoinData = context.stuinformation
                    .GroupJoin(context.Record,
                        student => student.StudentID,
                        record => record.StudentID,
                        (student, records) => new
                        {
                            StudentID = student.StudentID,
                            StudentName = student.FirstName + " " + student.LastName,
                            Age = student.Age,
                            Email = student.Email,
                            Phone = student.Phone,
                            Address = student.Address,
                            Marks = records.Select(r => new
                            {
                                RecordID = r.RecordID,
                                Tamil = r.Tamil,
                                English = r.English,
                                Maths = r.Maths,
                                Science = r.Science,
                                Social = r.Social
                            }).ToList()
                        })
                    .ToList();

                Console.WriteLine("\nGrouped Student Information using GroupJoin:");
                foreach (var student in groupJoinData)
                {
                    Console.WriteLine($"Student ID: {student.StudentID}");
                    Console.WriteLine($"Name: {student.StudentName}");
                    Console.WriteLine($"Age: {student.Age}");
                    Console.WriteLine($"Email: {student.Email}");
                    Console.WriteLine($"Phone: {student.Phone}");
                    Console.WriteLine($"Address: {student.Address}");
                    Console.WriteLine("Marks:");
                    foreach (var marks in student.Marks)
                    {
                        Console.WriteLine($"Record ID: {marks.RecordID}, Tamil: {marks.Tamil}, English: {marks.English}, Maths: {marks.Maths}, Science: {marks.Science}, Social: {marks.Social}");
                    }
                    Console.WriteLine("---------------------------------------------------");
                }
            }

        }

        //SELECT MANY METHOD //
        // IT WILL SELECT COMINATIONS VALUES NAMES AND CITIES ONLY//
        public static void Selectmanymethod()
        {

            string[] names = { "Karthik", "Mohan", "Manoj", "Hari", "Santhosh", "Sasi" };

            string[] cities = { "Arcot", "Vellore", "Chennai" };



            var nameandcity = names

                .SelectMany(name => cities, (name, city) => new { Name = name, City = city })

                .ToList();

            Console.WriteLine("Name and City Combination:");

            foreach (var data in nameandcity)
            {
                Console.WriteLine($"Name: {data.Name}, City: {data.City}");
            }
        }


        public static void Orderbythenbymethod()
        {
            using (var context = new StudentDbcontext())
            {
                var sortedStudents = context.stuinformation

                    .OrderBy(student => student.FirstName)

                    .ThenByDescending(student => student.Age)

                    .Select(student => new
                    {

                        StudentID = student.StudentID,

                        StudentName = student.FirstName + " " + student.LastName,

                        Age = student.Age,

                        Email = student.Email,


                        Phone = student.Phone,

                        Address = student.Address
                    })

                    .ToList();



                Console.WriteLine("\nSorted Student Information:");

                foreach (var student in sortedStudents)
                {
                    Console.WriteLine($"Student ID: {student.StudentID}");

                    Console.WriteLine($"Name: {student.StudentName}");
                    Console.WriteLine($"Email: {student.Email}");


                    Console.WriteLine($"Age: {student.Age}");


                    Console.WriteLine($"Phone: {student.Phone}");

                    Console.WriteLine($"Address: {student.Address}");

                    Console.WriteLine("---------------------------------------------------");
                }
            }


        }





        public static void Grpordthenordbymethod()
        {
            using (var context = new StudentDbcontext())
            {

                var groupedStudents = context.stuinformation
          .GroupBy(student => student.Address)
          .Select(group => new Example1
          {
              Address = group.Key,
              Students = group.OrderBy(student => student.FirstName)
                  .ThenByDescending(student => student.Age)
                  .Select(student => new Example2
                  {
                      StudentID = student.StudentID,
                      StudentName = student.FirstName + " " + student.LastName,
                      Age = student.Age,
                      Email = student.Email,
                      Phone = student.Phone
                  })
                  .ToList()

          })
                .ToList();








                Console.WriteLine("\nGrouped and Sorted Student Information:");

                foreach (var group in groupedStudents)
                {

                    Console.WriteLine($"\nAddress: {group.Address}");

                    Console.WriteLine("---------------------------------------------------");

                    foreach (var student in group.Students)
                    {

                        Console.WriteLine($"Student ID: {student.StudentID}");

                        Console.WriteLine($"Name: {student.StudentName}");

                        Console.WriteLine($"Email: {student.Email}");

                        Console.WriteLine($"Age: {student.Age}");

                        Console.WriteLine($"Phone: {student.Phone}");

                        Console.WriteLine("---------------------------------------------------");
                    }
                }
            }

        }


        //TOLOOKP METHOD //
        //   used to extract a set of key/value pairs from the source//
        public static void Tolookupmethod()
        {
            using (var context = new StudentDbcontext())
            {
                var lookupByAddress = context.stuinformation
                    .ToList()
                    .ToLookup(student => student.Address);

                Console.WriteLine("Grouped Students by Address:");
                foreach (var addressGroup in lookupByAddress)
                {
                    Console.WriteLine($"Address: {addressGroup.Key}");
                    Console.WriteLine("Students:");

                    foreach (var student in addressGroup)
                    {
                        Console.WriteLine($"Student Name: {student.FirstName}, Student ID: {student.StudentID}, Email: {student.Email}, Age: {student.Age}");
                    }
                    Console.WriteLine("---------------------------------------------------");
                }
            }
        }
        //one sequence is appended into another sequence this is known to be as concat//
        public static void Concatmethod()
        {
            using (var context = new StudentDbcontext())
            {

                var students1 = context.stuinformation.Where(s => s.Address == "Walaja");

                var students2 = context.stuinformation.Where(s => s.Address == "Chennai");


                var allStudents = students1.Concat(students2).ToList();

                Console.WriteLine("Concatenated Student Data:");
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"Student Name: {student.FirstName}, Student ID: {student.StudentID}, Email: {student.Email}, Age: {student.Age}, Address: {student.Address}");
                }
            }
        }



        //to check if at least one element of a collection satisfies a given condition or not //
        public static void Anymethod()
        {
            using (var context = new StudentDbcontext())
            {

                bool hasStudentsFromWalaja = context.stuinformation.Any(s => s.Address == "Walaja");

                Console.WriteLine(hasStudentsFromWalaja
                    ? "There are students from Walaja."
                    : "No students from Walaja.");


                bool hasOlderStudents = context.stuinformation.Any(s => s.Age > 25);

                Console.WriteLine(hasOlderStudents
                    ? "There are students older than 25."
                    : "No students older than 25.");
            }
        }

        //checks whether a string contains a sequence of characters//
        public static void Conmethods()
        {
            using (var context = new StudentDbcontext())
            {
                var temp1 = new List<int> { 2, 4, 5 };
                var temp2 = new List<bool> { true, false, true };
                ;
                // Check if there are students in Walaja or Arcot
                var targetAddresses = new List<string> { "Walaja", "Arcot" };
                var hasStudentsInTargetAddresses = context.stuinformation.Any(s => targetAddresses.Contains(s.Address));

                Console.WriteLine(hasStudentsInTargetAddresses == true
                    ? "There are students in Walaja or Arcot."
                    : "No students in Walaja or Arcot.");

                // Correct way to check if an email exists
                string emailToCheck = "student@example.com";
                bool emailExists = context.stuinformation.Any(s => s.Email == emailToCheck); // ✅ More efficient

                Console.Write(emailExists
                    ? $"Email {emailToCheck} exists in the database."
                    : $"Email {emailToCheck} does not exist in the database.");
            }
        }

        public static void Skipmethod()
        {
            using (var context = new StudentDbcontext())
            {
                var students = context.stuinformation
                                      .OrderBy(s => s.StudentID)
                                      .ToList()
                                      .Skip(5)
                                      .ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"Name: {student.FirstName}, City: {student.Address}");
                }
            }
        }


        public static void Skipwhilemethod()
        {
            using (var context = new StudentDbcontext())
            {
                var students = context.stuinformation
                    // .OrderBy(s=>s.StudentID)
                    .ToList()
                    .SkipWhile(s => s.Age <= 18)
                    .ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"Name: {student.FirstName}, Age: {student.Age}");
                }


            }
        }

        //skip 13 set of records untill it end come//
        public static void Skipsmethod()
        {
            using (var context = new StudentDbcontext())
            {
                int pageSize = 13;
                int pageIndex = 0;

                while (true)
                {
                    var students = context.stuinformation

                                           .AsEnumerable()

                                          .Skip(pageIndex * pageSize)
                                          .Take(pageSize)
                                          .ToList();

                    if (students.Count == 0)
                    {
                        Console.WriteLine("No more records.");
                        break;
                    }

                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, City: {student.Address}");
                    }

                    pageIndex++; // Move to the next batch
                    Console.WriteLine("\nPress ENTER for the next 13 records, or type 'exit' to stop...");

                    string input = Console.ReadLine() ?? ""; // Ensure input is never null
                    //if (input.Trim().ToLower() == "exit") break; // Trim to handle extra spaces
                }
            }
        }

        public static void Defaultisemptymethod()
        {
            using (var context = new StudentDbcontext())
            {
                var students = context.stuinformation
                                       .ToList()
                                      .DefaultIfEmpty(new StudentInformation { FirstName = "No Data Available" })
                                      .ToList();


                foreach (var student in students)
                {
                    Console.WriteLine($"Name: {student.FirstName}");
                }
            }
        }

        public static void RangeMethod()
        {
            using var context = new StudentDbcontext();

            Console.Write("Enter start age: ");
            int startAge = int.Parse(Console.ReadLine());

            Console.Write("Enter end age: ");
            int endAge = int.Parse(Console.ReadLine());

            var ageRange = Enumerable.Range(startAge, endAge - startAge + 1);

            var students = context.stuinformation
                                  .ToList()
                                  .Where(s => ageRange.Contains(s.Age))
                                  .ToList();

            students.ForEach(s => Console.WriteLine($"Name: {s.FirstName}, Age: {s.Age}"));
        }

        //select only Age field and remove duplicates values //
        public static void Distinctmethod()
        {
            using (var context = new StudentDbcontext())
            {
                var students = context.stuinformation

                     .Select(s => s.Age)
                     .Distinct()
                    .ToList();

                foreach (var student in students)
                { Console.WriteLine($"Age: {student}"); }


            }

        }
        // Selects multiple field values and remove duplicates //
        public static void DistinctbyMethod()
        {
            using (var context = new StudentDbcontext())
            {
                var students = context.stuinformation
                    .AsEnumerable()
                    .DistinctBy(s => s.Age)
                    .ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"Age: {student.Age}, Name: {student.FirstName}, Address:{student.Address}");


                }
            }
        }



        //Here two tables comparing with studentID then it will return values that is not present in the table 2 compared to table1//
        public static void ExceptMethod()
        {
            using var context = new StudentDbcontext();


            var studenttable1 = context.stuinformation
            .Select(s => s.StudentID);
            var studenttable2 = context.Record
            .Select(s => s.StudentID);


            var uniqueStudentIds = studenttable1
            .Except(studenttable1)
            .ToList();

            Console.WriteLine("Student ID present in Table1 but not in Table2:");
            foreach (var id in uniqueStudentIds)
            {
                Console.WriteLine(id);
            }
        }

        //sequence equal method check if the two studentid are sequently crt or not if crt return true otherwise false//
        public static void Sequencemethod()
        {
            using var context = new StudentDbcontext();


            var Table1 = context.stuinformation

                .Select(s => s.StudentID)
                 .ToList();

            var Table2 = context.Record

                .Select(s => s.StudentID)
                .ToList();


            bool Sequencemeth = Table1.SequenceEqual(Table2);

            if (Sequencemeth)
            {
                Console.WriteLine("Both tables have the same StudentIDs in the same order");
            }
            else
            {
                Console.WriteLine("No sequence found The StudentIDs in both tables are different");
            }
        }
        //this method returns the element at a specified index in a sequence. //
        public static void Elementatmethod()
        {
            using var context = new StudentDbcontext();
            {



                var sortedStudents = context.Record
                   .OrderByDescending(s => s.English)
                   .ToList();

                if (sortedStudents.Count > 0)
                {

                    var topStudent = sortedStudents.ElementAt(0);



                    Console.WriteLine("Student who scored the highest marks in English:");
                    Console.WriteLine($"ID: {topStudent.StudentID}");

                    Console.WriteLine($"English Marks: {topStudent.English}");

                    Console.WriteLine($"Maths Marks: {topStudent.Maths}");
                    Console.WriteLine($"Science Marks: {topStudent.Science}");
                }
                else
                {
                    Console.WriteLine("No student records found.");
                }
            }
        }

        public static void Elementatordefault()
        {
            using var context = new StudentDbcontext();
            {
                var students = context.Record
                    .OrderByDescending(s => s.English)
                    .ToList();


                var topStudent = students.ElementAtOrDefault(0); 
               

                



                if (topStudent == null)
                {
                    topStudent = new Records { StudentID = 0, English = 0, Maths = 0, Science = 0 };
                }



                
                

                   
                Console.WriteLine("Student who scored the highest marks in English:");
                
                
                Console.WriteLine($"ID: {topStudent.StudentID}");
                
                Console.WriteLine($"English Marks: {topStudent.English}");
                
                Console.WriteLine($"Maths Marks: {topStudent.Maths}");
                
                Console.WriteLine($"Science Marks: {topStudent.Science}");
                
                

            }



        }

public static void Firstmethod()
        {
            using var context = new StudentDbcontext();
            {
                var student = context.stuinformation
                    .First();




                Console.WriteLine($"StudentID: {student.StudentID}, Name: {student.FirstName}, Address: {student.Address}, Age: {student.Age}");


            }
        }


public static void Lastmethod()
        {
            using var context = new StudentDbcontext();
            {
                var student = context.stuinformation
                    .ToList()
                    .Last();

                Console.WriteLine($"StudentID:{student:StudentID},Name:{student.FirstName},Age:{student.Age},Address:{student.Address}");
            }
        }



    }

}



















