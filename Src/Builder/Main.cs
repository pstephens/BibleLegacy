using System;
using System.IO;

namespace Bible
{
    internal class MainClass
    {
        public static void Main()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "BibleTemp");
            Directory.CreateDirectory(tempPath);

            const string outputFile = @"..\Data\Av.bible";
            string outputPath = Path.GetDirectoryName(outputFile);
            Directory.CreateDirectory(outputPath);

            var bible = new BibleAccum(@"..\Normalized\Kjv3.txt",
                                       tempPath, outputFile);

            bible.Parse();
            bible.Process();
            bible.Write();

            DisplayStats(bible.Bible, bible.WordIndex);

            Console.WriteLine("Press the enter key to continue...");
            Console.ReadLine();
        }

        public static void DisplayStats(Bible bible, WordIndex idx)
        {
            // Calc some stats
            Int32 minVerseRef = -1,
                  maxVerseRef = -1,
                  minVerseCount = -1,
                  maxVerseCount = -1,
                  minChapCount = -1,
                  maxChapCount = -1,
                  minVerseSize = -1,
                  maxVerseSize = -1;
            for (Int32 i = 0; i < bible.Books.Count; ++i)
            {
                Book b = bible.Books[(BookName) i];
                if (minChapCount == -1 || minChapCount > b.Chapters.Count)
                    minChapCount = b.Chapters.Count;
                if (maxChapCount == -1 || maxChapCount < b.Chapters.Count)
                    maxChapCount = b.Chapters.Count;

                for (Int32 j = 0; j < b.Chapters.Count; ++j)
                {
                    Chapter ch = b.Chapters[j];
                    if (minVerseCount == -1 || minVerseCount > ch.Verses.Count)
                        minVerseCount = ch.Verses.Count;
                    if (maxVerseCount == -1 || maxVerseCount < ch.Verses.Count)
                        maxVerseCount = ch.Verses.Count;

                    for (Int32 k = 0; k < ch.Verses.Count; ++k)
                    {
                        Verse v = ch.Verses[k];
                        if (minVerseSize == -1 || minVerseSize > v.VerseData.Length)
                            minVerseSize = v.VerseData.Length;
                        if (maxVerseSize == -1 || maxVerseSize < v.VerseData.Length)
                            maxVerseSize = v.VerseData.Length;
                    }
                }
            }

            // Calc some word stats
            Int32 verseRefTot = 0, wordData = 0;
            var words = idx.CaseSensitiveWords.GetAllWords();
            foreach (var t in words)
            {
                if (minVerseRef == -1 || minVerseRef > t.VerseRefCount)
                    minVerseRef = t.VerseRefCount;
                if (maxVerseRef == -1 || maxVerseRef < t.VerseRefCount)
                    maxVerseRef = t.VerseRefCount;
                verseRefTot += t.VerseRefCount;
                wordData += t.Word.Length;
            }
            Console.WriteLine("CaseSensitiveIndex:");
            Console.WriteLine(" Words: {0}", words.Length);
            Console.WriteLine(" Verse References: {0}", verseRefTot);
            Console.WriteLine(" Total word bytes: {0}", wordData);
            Console.WriteLine(" Max verse references per word: {0}", maxVerseRef);
            Console.WriteLine(" Min verse references per word: {0}", minVerseRef);

            verseRefTot = 0;
            wordData = 0;
            minVerseRef = -1;
            maxVerseRef = -1;
            words = idx.CaseInsensitiveWords.GetAllWords();
            foreach (WordObject t in words)
            {
                if (minVerseRef == -1 || minVerseRef > t.VerseRefCount)
                    minVerseRef = t.VerseRefCount;
                if (maxVerseRef == -1 || maxVerseRef < t.VerseRefCount)
                    maxVerseRef = t.VerseRefCount;
                verseRefTot += t.VerseRefCount;
                wordData += t.Word.Length;
            }
            Console.WriteLine("CaseInsensitiveIndex:");
            Console.WriteLine(" Words: {0}", words.Length);
            Console.WriteLine(" Verse References: {0}", verseRefTot);
            Console.WriteLine(" Total word bytes: {0}", wordData);
            Console.WriteLine(" Max verse references per word: {0}", maxVerseRef);
            Console.WriteLine(" Min verse references per word: {0}", minVerseRef);

            // Print stats
            Console.WriteLine("Max chapters per book: {0}", maxChapCount);
            Console.WriteLine("Min chapters per book: {0}", minChapCount);
            Console.WriteLine("Max verses per chapter: {0}", maxVerseCount);
            Console.WriteLine("Min verses per chapter: {0}", minVerseCount);
            Console.WriteLine("Max verse length: {0}", maxVerseSize);
            Console.WriteLine("Min verse length: {0}", minVerseSize);
        }
    }
}