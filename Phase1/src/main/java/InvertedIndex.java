import java.util.ArrayList;
import java.util.Collections;

public interface InvertedIndex {
    static ArrayList<String> getResult() {
        HashedInvertedIndex.tokenizeContentsOfDocs();

        Collections.sort(HashedInvertedIndex.tokens);


        //iterate the tokensArray to find the identical words to merge them
        HashedInvertedIndex.mergeIdenticalWordsAndCreateHashTableOfWords();


        //one word search
//        Scanner scanner = new Scanner(System.in);
//        String searchingTerm = scanner.nextLine();
//        if (table.get(searchingTerm.toLowerCase()).size() != 0) {
//            for (String doc : table.get(searchingTerm.toLowerCase())) {
//                System.out.println(doc);
//            }
//        }







        ArrayList<String> tempResult;



        HashedInvertedIndex.initiateResult(HashedInvertedIndex.table);
        tempResult = HashedInvertedIndex.result;
        HashedInvertedIndex.result = HashedInvertedIndex.getNotSignedDocs(HashedInvertedIndex.table, tempResult);


        HashedInvertedIndex.result = HashedInvertedIndex.plusDocs(HashedInvertedIndex.table);
        System.out.println(HashedInvertedIndex.result);

        HashedInvertedIndex.result = HashedInvertedIndex.minusDocs(HashedInvertedIndex.table);

        return HashedInvertedIndex.result;

    }

    static void addToken(Token token) {
        HashedInvertedIndex.tokens.add(token);
    }

    static void addTUnSignedWords(String unSignedWord) {
        HashedInvertedIndex.unSignedWords.add(unSignedWord);
    }

    static void setPlusSignedWords(String plusSignedWord) {
        HashedInvertedIndex.plusSignedWords.add(plusSignedWord);
    }

    static void setMinusSignedWords(String minusSignedWord) {
        HashedInvertedIndex.minusSignedWords.add(minusSignedWord);
    }
}
