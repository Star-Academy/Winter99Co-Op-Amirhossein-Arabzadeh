import java.util.*;

public interface InvertedIndex {

    //produces the result ArrayList for the search action
    ArrayList<String> getResult();

    //initiates the result ArrayList for the first unsigned word; it takes the hashTable used for
    //maintain words as key and containing docs list as value
    void initiateResult(HashMap<String, ArrayList<String>> table);

    //merges tokenized words from the docs content
    void mergeIdenticalWordsAndCreateHashTableOfWords();

    //tokenizes words in docs contents
    void tokenizeContentsOfDocs();

    //returns docs with contain unsigned searching words, tempResult is use to solve the problem of
    //concurrent modification on the result ArrayList
    ArrayList<String> getNotSignedDocs(HashMap<String, ArrayList<String>> table, ArrayList<String> tempResult);

    //ands result ArrayList with list of docs containing plus signed words
    ArrayList<String> plusDocs(HashMap<String, ArrayList<String>> table);

    //ands result ArrayList with set of docs containing plusSigned words
    ArrayList<String> andResultSet(Set<String> docsWitchHasPlusWords);

    //look likes plusDocs()
    ArrayList<String> minusDocs(HashMap<String, ArrayList<String>> table);

    //look likes andResultSet()
    ArrayList<String> minusResultSet(Set<String> anotherSet);

    //creates sets of plus/minus signed words
    //uses partition witch is an ArrayList containing words with minus/plus/nothing signed words
    void createSetOfDifferentModeledInputs(HashMap<String, ArrayList<String>> table, Set<String> docsWitchHasMinusWords, ArrayList<String> partition);

    //adds tokens to myTokens Arraylist witch contains tokens of the words of the contents of a doc
    void addToken(MyToken myToken);

    //adds to ArrayList of minusSigned words of input of user
    void addToUnSignedWords(String unSignedWord);

    //looks like addToUnSignedWords
    void addToPlusSignedWords(String plusSignedWord);

    //looks like addToUnSignedWords
    void addToMinusSignedWords(String minusSignedWord);


}

