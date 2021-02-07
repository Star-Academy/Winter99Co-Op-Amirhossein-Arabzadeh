import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;

public class MyFileReader {
    private static StringBuilder content = new StringBuilder();
    public static void readFiles() throws IOException, IOException {
        // create instance of directory
        File dir = new File("C:\\Users\\Amirhossein\\Desktop\\EnglishData");

        // create object of PrintWriter for output file
        //PrintWriter pw = new PrintWriter("output.txt");


        // Get list of all the files in form of String Array
        String[] fileNames = dir.list();

        // loop for reading the contents of all the files
        // in the directory GeeksForGeeks
        for (String fileName : fileNames) {
            System.out.println("Reading from " + fileName);

            // create instance of file from Name of
            // the file stored in string Array
            File f = new File(dir, fileName);

            // create object of BufferedReader
            BufferedReader br = new BufferedReader(new java.io.FileReader(f));
            //pw.println("Contents of file " + fileName);

            // Read from current file
            String line = br.readLine();
            while (line != null) {

                // write to the output file
                //pw.println(line);
                //content.append(line);
                for (String word : line.split("\\W+")) {
                    Token token = new Token(word.toLowerCase());
                    token.addToDocs(fileName);
                    InvertedIndex.addTokens(token);
                }
                line = br.readLine();
            }
            //pw.flush();
        }
        System.out.println("Reading from all files" +
                " in directory " + dir.getName() + " Completed");
    }

    public static StringBuilder getContent() {
        return content;
    }
}
