import java.util.*;

public class TokensTableMerger implements Merger {
    public HashMap<String, List<String>> createHashTableOfWordsFromSortedList(List<DocsWordOccurrence> tokens) {
        HashMap<String, List<String>> table = new HashMap<>();
        for (WordOccurrence token: tokens){
            if (!table.containsKey(token.getTerm())){
                List<String> docs = new ArrayList<>();
                docs.add(token.getDoc());
                table.put(token.getTerm(), docs);
                continue;
            }
            table.get(token.getTerm()).add(token.getDoc());
        }
        return table;
    }
}
