open System

module StringHelper =
    let mergeLines (startChar: string) (input : string seq) =
        let result = new ResizeArray<string>()

        for line in input do
            if line.StartsWith(startChar) then
                result.Add(line)
            else
                if result.[result.Count - 1].StartsWith(startChar) then 
                    result.Add(String.Empty)
                result.[result.Count - 1] <- result.[result.Count - 1] + line

        result

    let mergeFASTA (input: string seq) = mergeLines ">" input

    let extractFASTAId (header:string) =
        header.Substring(header.IndexOf("_") + 1)

    ///Returns true if 'pattern' aligns to 'source' starting at the first character of 'source'
    let rec matchExactly (source: 'a list) (pattern: 'a list) =
        match pattern, source with
        | [], [] | [], _  -> true
        | pattChar :: pattRest , srcChar :: srcRest -> if pattChar <> srcChar then 
                                                        false 
                                                       else 
                                                        matchExactly srcRest pattRest
        | _, [] -> false
    
    ///Returns the start index of all occurrences of 'pattern' in 'source'. Search starts at 'index'
    let rec findMatches (source: 'a list) (pattern: 'a list) index =
        match source with
        | [] -> []
        | character :: rest -> match matchExactly source pattern with 
                                  | true  ->  index :: (findMatches rest pattern (index + 1)) 
                                  | false ->  findMatches rest pattern (index + 1)
