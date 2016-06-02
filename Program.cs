//Meeshaan Shah
//CPSC 351
//Partner: Ani Khalili
//Code was developed together in tandom with Ani
//Code does a boolean reduction given input of a truthtable up to 8 inputs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace MultiDimKmap
{ 
    class Program
    {
        static void Main(string[] args)
        {

            double numInputs = 0;
            string line = "";
            string inputs = "";
            string result = "";
            int arrayCounter = 0;
            int fileCounter = 0;
            List<string> trueBits = new List<string>();
            StreamReader inFile = new StreamReader("test.txt");//opens the file

            List<SortedSet<string>> finalResult = new List<SortedSet<string>>();

            List<SortedSet<string>> setOfTwo = new List<SortedSet<string>>();
            List<SortedSet<string>> setOfFour = new List<SortedSet<string>>();
            List<SortedSet<string>> setOfEight = new List<SortedSet<string>>();
            List<SortedSet<string>> setOfSixteen = new List<SortedSet<string>>();
            List<SortedSet<string>> setOfThirtyTwo = new List<SortedSet<string>>();
            List<SortedSet<string>> setOfSixtyFour = new List<SortedSet<string>>();
            List<SortedSet<string>> setOfOneTwentyEight = new List<SortedSet<string>>();
            List<SortedSet<string>> setOfTwoFiftySix = new List<SortedSet<string>>();
            List<SortedSet<string>> fiveTwelve = new List<SortedSet<string>>();
           

            List<string> setOfOneAdded = new List<string>();
            List<SortedSet<string>> setsAdded = new List<SortedSet<string>>();

            line = inFile.ReadLine();//gets the number of inputs
            numInputs = Convert.ToDouble(line);
            int numBitsInt = Convert.ToInt32(numInputs);
            int size =Convert.ToInt32( Math.Pow(2.0, numInputs));
            fileCounter = size;

            while (fileCounter>0)
            {
                line = inFile.ReadLine().Replace(" ", "");//remove spaces
                inputs = line.Substring(0,numBitsInt);//get the inputs bits
                result = line.Substring(numBitsInt,1); //get the output value              
               if (result == "1")
               {
                   trueBits.Add(inputs);
               }
                arrayCounter = Convert.ToInt32(inputs, 2);//find the base 10 value of the inputs

                --fileCounter;

                //reset variables
                line = "";
                inputs = "";
                result = "";
                arrayCounter = 0;
            }
  
            //initially putting numbers in sets
            if (trueBits.Count == 0)
            {
                Console.WriteLine("The Equation is 0.");
            }
            else if (trueBits.Count == size)
            { Console.WriteLine("The Equation is 1."); }
            else
            {

                for (int curr = 0; curr < trueBits.Count - 1; ++curr)
                {
                    for (int next = 1; (curr + next) < trueBits.Count; ++next)
                    {
                        string num1 = trueBits[curr];
                        string num2 = trueBits[curr + next];
                        compareNum(numInputs, num1, num2, ref setOfTwo, ref finalResult, ref setOfOneAdded, ref trueBits);
                    }
                }
                //after numbers have been put in sets, check to see if there are any numbers that don't have anything adjacent to them, and add them to final result
                for (int i = 0; i < trueBits.Count; ++i)
                {
                    string tempNum = trueBits[i];
                    SortedSet<string> tempSet = new SortedSet<string>();

                    if (!setOfOneAdded.Contains(tempNum))//the number is not in the set, so it must be part of the final result
                    {
                        tempSet.Add(tempNum);
                        finalResult.Add(tempSet);
                    }
                }

                //check to see if we can make sets of four and so on...
                if (setOfTwo.Count > 1)
                {
                    //check for lone groups of 2 to add to finalResult
                    makeCircle(ref setOfTwo, ref setOfFour, numInputs, ref finalResult);
                    findLonewolf(ref setOfTwo, ref setOfFour, ref finalResult);
                    //Console.WriteLine("Done Four");
                }
                else if (setOfTwo.Count == 1)//add to final result
                { finalResult.Add(setOfTwo.ElementAt(0)); }

                if (setOfFour.Count > 1)
                {
                    makeCircle(ref setOfFour, ref setOfEight, numInputs, ref finalResult);
                    findLonewolf(ref setOfFour, ref setOfEight, ref finalResult);
                    // Console.WriteLine("Done Eight");
                }
                else if (setOfFour.Count == 1) { finalResult.Add(setOfFour.ElementAt(0)); }

                if (setOfEight.Count > 1)
                {
                    makeCircle(ref setOfEight, ref setOfSixteen, numInputs, ref finalResult);
                    findLonewolf(ref setOfEight, ref setOfSixteen, ref finalResult);
                    //Console.WriteLine("Done 16");
                }
                else if (setOfEight.Count == 1) { finalResult.Add(setOfEight.ElementAt(0)); }

                if (setOfSixteen.Count > 1)
                {
                    makeCircle(ref setOfSixteen, ref setOfThirtyTwo, numInputs, ref finalResult);
                    findLonewolf(ref setOfSixteen, ref setOfThirtyTwo, ref finalResult);
                    //Console.WriteLine("Done 32");
                }
                else if (setOfSixteen.Count == 1) { finalResult.Add(setOfSixteen.ElementAt(0)); }

                if (setOfThirtyTwo.Count > 1)
                {
                    makeCircle(ref setOfThirtyTwo, ref setOfSixtyFour, numInputs, ref finalResult);
                    findLonewolf(ref setOfThirtyTwo, ref setOfSixtyFour, ref finalResult);
                    //Console.WriteLine("Done 64");
                }
                else if (setOfThirtyTwo.Count == 1) { finalResult.Add(setOfThirtyTwo.ElementAt(0)); }

                if (setOfSixtyFour.Count > 1)
                {
                    makeCircle(ref setOfSixtyFour, ref setOfOneTwentyEight, numInputs, ref finalResult);
                    findLonewolf(ref setOfSixtyFour, ref setOfOneTwentyEight, ref finalResult);
                    //Console.WriteLine("Done 128");
                }
                else if (setOfSixtyFour.Count == 1) { finalResult.Add(setOfSixtyFour.ElementAt(0)); }

                if (setOfOneTwentyEight.Count > 1)
                {
                    makeCircle(ref setOfOneTwentyEight, ref setOfTwoFiftySix, numInputs, ref finalResult);
                    findLonewolf(ref setOfOneTwentyEight, ref setOfTwoFiftySix, ref finalResult);
                    //Console.WriteLine("Done 256");
                }
                else if (setOfOneTwentyEight.Count == 1) { finalResult.Add(setOfOneTwentyEight.ElementAt(0)); }

                //This code caused an error in certain cases. If small input file was all 1, it wasn't getting added to the final result
               /* if (setOfTwoFiftySix.Count == 1)
                {
                    finalResult.Clear();

                    Console.WriteLine("The equation is 1.");
                }
                else
                {*/
                    Console.WriteLine("Final Equation");
                    printFinalResult(ref finalResult);
                //}
            }
          

        }// end of main

        //Functions:

        static void compareNum(double bitSize, string num1, string num2, ref List<SortedSet<string>> sets, ref List<SortedSet<string>> final, ref List<string> numberAddedToSet, ref List<string> listOfSingles)
        {
            int numDiff = 0;
            SortedSet<string> temp = new SortedSet<string>(); //make sets

            for (int i = 0; i < bitSize; ++i)
            {
                if (num1[i] != num2[i])//comparing bits
                {                    
                    ++numDiff; //keeping track of the number of differences
                }
            }

            if (numDiff == 1) //if the bits only differ by one, then they are adjacent
            {
                temp.Add(num1);//add num1 to SortedSet temp
                temp.Add(num2);//add num2 to SortedSet temp
  
                if (numberAddedToSet.Contains(num1) && numberAddedToSet.Contains(num2))
                {//do nothing
                }
                    else{
                     
                        numberAddedToSet.Add(num1);
                       
                        numberAddedToSet.Add(num2);
                      
                        sets.Add(temp);

               }



                  
            }


        }//end of function

        static void compareSets(double bitSize, SortedSet<string> set1, SortedSet<string> set2, ref List<SortedSet<string>> setOfGroups, ref List<SortedSet<string>> final, ref List<SortedSet<string>> setTwo)
        {
            int numDiff = 0;                     
            List<int> diffs = new List<int>();
            List<int> spotChange = new List<int>();
            bool isAdjacent = true;
            bool sameLoc = true;
            

            for (int i = 0; i < set1.Count; ++i ) 
            {
                string num1 = set1.ElementAt(i);
                string num2 = set2.ElementAt(i);
                
                for (int j = 0; j < bitSize; ++j)
                {
                    if ( num1[j] != num2[j])
                    {
                        ++numDiff;
                        spotChange.Add(i);
                    }
                }
                diffs.Add(numDiff);
                numDiff = 0;
                
            }

            //checking the list diffs for any differences that are not 1
            foreach (int number in diffs)
            {
                if (number != 1)
                {
                    isAdjacent = false;
                    break;
                }
            }

            //checking is the difference is in the same dimension
            foreach(int d in spotChange)
            {
                if (spotChange[0] != d)
                {
                    sameLoc = false;
                }
                break;
            }

            if ((isAdjacent == true) && (sameLoc == true)) //if the bits only differ by one and in the same dimension, then they are adjacent
            {
                SortedSet<string> temp = new SortedSet<string>(set1); //make sets
                temp.UnionWith(set2);                                            

                //checking for dupes                               
               if (setOfGroups.Count == 0)
                {
                    setOfGroups.Add(temp);
                    
                }

                else
                {
                    int newCounter = 0;                    
                    for (int k = 0; k < setOfGroups.Count; ++k)//iterates over the number of sets in the list
                    {
                        int duplicateCounter = 0;
                        SortedSet<string> temp2 = setOfGroups[k];//gets the set                         
                        //compare the sets
                        for (int r = 0; r < temp.Count; ++r)//iterates over the size of the set
                        {
                            if (temp2.Contains(temp.ElementAt(r)) == true)
                            {                                
                                ++duplicateCounter;
                            }                      
                            
                        }//if duplicateCounter is the size of the set, break and don't add                   
                       if(duplicateCounter == temp.Count)
                        {
                            ++newCounter; 
                        }
                  
                    }

                    if (newCounter == 0)
                    {
                        setOfGroups.Add(temp);

                    }
                }
                
            }           
        }
//========================================================================================================
//========================================================================================================
        static void findLonewolf(ref List<SortedSet<string>> smallerPairs, ref List<SortedSet<string>> biggerPairs, ref List<SortedSet<string>> finalR)
        {    
            List<SortedSet<string>> setToRemove = new List<SortedSet<string>>();

            foreach (SortedSet<string> s in smallerPairs)
            {
                for (int j = 0; j < biggerPairs.Count; ++j)
                {
                    if (s.IsSubsetOf(biggerPairs[j]))
                    {                                                
                        setToRemove.Add(s);
                    }
                }
            }

            for (int k = 0; k < setToRemove.Count; ++k)
            {
                smallerPairs.Remove(setToRemove[k]);
            }

            
            if (smallerPairs.Count > 0)
            {
                foreach(SortedSet<string> s in smallerPairs)
                {
                    finalR.Add(s);//adding uncircled pairs to final result
                }
            }
        }
//========================================================================================================
//========================================================================================================
        static void printListOfSets(ref List<SortedSet<string>> setOfGroups)
        {
            for (int i = 0; i < setOfGroups.Count; ++i)//outputing what is in the list of sets
            {
                string temp = string.Join(", ", setOfGroups[i]);
                Console.WriteLine(temp);

            }
        }
//========================================================================================================
//========================================================================================================
        static void printList(ref List<string> singleList)
        {
            foreach (string s in singleList)
            {                
                Console.WriteLine(s);
            }
        }
//========================================================================================================
//========================================================================================================
        static void makeCircle(ref List<SortedSet<string>> smallSet, ref List<SortedSet<string>> biggerSet, double numberOfInputs, ref List<SortedSet<string>> finalEq)
        {
            for (int curr = 0; curr < smallSet.Count - 1; ++curr)
            {
                for (int next = 1; (curr + next) < smallSet.Count; ++next)
                {
                    SortedSet<string> temp1 = new SortedSet<string>();
                    SortedSet<string> temp2 = new SortedSet<string>();
                    bool contDupe = false;
                    temp1 = smallSet[curr];
                    temp2 = smallSet[curr + next];

                    //needed for removing groups of three
                    foreach (string s in temp1)
                    {
                        if (temp2.Contains(s))
                        {
                            contDupe = true;
                        }
                    }

                    //can we make a set of four?
                    if (contDupe == false)
                    {
                        compareSets(numberOfInputs, temp1, temp2, ref biggerSet, ref finalEq, ref smallSet);
                    }
                }
            } //end of for loop 
        }
//========================================================================================================
//========================================================================================================
        static void printFinalResult(ref List<SortedSet<string>> finalEquation )
        {
              String[] characters = new string[] {"A","B","C","D","E","F","G","H"};
              int didChange = 0;
              StringBuilder equation = new StringBuilder();

              if ((finalEquation.Count == 1) && (finalEquation.ElementAt(0).Count==1))
              {
                  for (int h = 0; h < finalEquation.ElementAt(0).ElementAt(0).Length; ++h)
                  {
                      string temp = finalEquation.ElementAt(0).ElementAt(0);
                      if (temp[h] == '0')
                      {
                          string output = "~" + characters[h];
                          equation.Append(output);
                          //Console.Write("~" + characters[h]);                          
                      }
                      else
                      {
                          string output = characters[h];
                          equation.Append(output);
                         // Console.Write(characters[h]);
                      }
                  }
                  equation.Append(" + ");
              }
              else
              {

                  foreach (SortedSet<string> s in finalEquation)//looping over list
                  {
                      //compare bits of elements
                      if (s.Count == 1)
                      {                                                    
                          for (int h = 0; h < s.ElementAt(0).Count(); ++h)
                          {
                              string temp = s.ElementAt(0);
                              if (temp[h] == '0')
                              {
                                  string output = "~" + characters[h];
                                  equation.Append(output);
                                  //Console.Write("~" + characters[h]);
                              }
                              else
                              {
                                  string output = characters[h];
                                  equation.Append(output);
                                  //Console.Write(characters[h]);
                              }
                          }
                          equation.Append(" + ");
                          //Console.Write(" + ");
                      }
                      else
                      {

                          string num1 = s.ElementAt(0);

                          for (int k = 0; k < num1.Length; ++k)
                          {
                              for (int next = 1; next < s.Count; ++next)
                              {
                                  string num2 = s.ElementAt(next);
                                  if (!(num1[k] == num2[k]))
                                  {
                                      ++didChange;
                                  }
                              }

                              if (didChange == 0)
                              {
                                  if (num1[k] == '0')
                                  {
                                      string output = "~" + characters[k];
                                      equation.Append(output);
                                      //Console.Write("~" + characters[k]);
                                  }
                                  else
                                  {
                                      string output = characters[k];
                                      equation.Append(output);
                                      //Console.Write(characters[k]);
                                  }
                                  //equation.Append(" + ");
                              }
                              didChange = 0;

                          }
                          equation.Append(" + ");
                          //Console.Write(" + ");

                      }//end of else

                  }
              }

              //Console.WriteLine(equation.ToString());
            string holder = equation.ToString();
            string finalEquationOutput = holder.Substring(0, holder.Length - 3);
            Console.WriteLine(finalEquationOutput);
        }

    }
}
