import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;

public class MyFileReader {
    private static StringBuilder content = new StringBuilder();
    public static void readFiles() throws IOException, IOException {
        // create instance of directory
        File dir = new File("C:\\Users\\Amirhossein\\Desktop\\EnglishData");

        // Get list of all the files in form of String Array
        String[] fileNames = dir.list();

        // loop for reading the contents of all the files
        // in the wanted directory
        for (String fileName : fileNames) {
            System.out.println("Reading from " + fileName);

            // create instance of file from Name of
            // the file stored in string Array
            File f = new File(dir, fileName);

            // create object of BufferedReader
            BufferedReader br = new BufferedReader(new java.io.FileReader(f));

            // Read one line from current file
            String line = br.readLine();
            while (line != null) {
                //read line by line and add tokens to tokens array in the invertedIndex class
                for (String word : line.split("\\W+")) {
                    Token token = new Token(word.toLowerCase());
                    token.addToDocs(fileName);
                    InvertedIndex.addTokens(token);
                }
                line = br.readLine();
            }
        }
        System.out.println("Reading from all files" +
                " in directory " + dir.getName() + " Completed");
    }

}
