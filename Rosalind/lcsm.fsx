
// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

let input = "/tmp/rosalind_lcsm.txt"
let output = "/tmp/rosalind_lcsmresult.txt"

open System.IO
open System
open System.Linq

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

let merger = mergeLines ">"


let read = File.ReadLines(input)
            |> merger
            |> Seq.chunkBySize 2
            |> Seq.map (fun item -> (int <| item.[0].Substring(item.[0].IndexOf("_") + 1)), item.[1] |> List.ofSeq)
            |> Map.ofSeq

let rec matchExactly (source: 'a list) (pattern: 'a list) =
    match pattern, source with
    | [], [] | [], _  -> true
    | pattChar :: pattRest , srcChar :: srcRest -> if pattChar <> srcChar then 
                                                    false 
                                                   else 
                                                    matchExactly srcRest pattRest
    | _, [] -> false

let rec findMatches (source: 'a list) (pattern: 'a list) index =
    match source with
    | [] -> []
    | character :: rest -> match matchExactly source pattern with 
                              | true  ->  index :: (findMatches rest pattern (index + 1)) 
                              | false ->  findMatches rest pattern (index + 1)

let produceAllSubstringsOfGivenSize (input : 'a list) (size : int) =
    if 
        size > input.Length then Seq.empty
    else
        let nrOfOutput = input.Length - size
        seq {
            for i in 0 .. nrOfOutput do
                yield input.[i..i+size-1]
            }

let rec findCommonSubstringsOfGivenSize (input:Map<int, char list>) (toFind: char list) (size:int) = 
    if size = 1 then 
        Seq.empty
    else
        let res = produceAllSubstringsOfGivenSize toFind size
                    |> Seq.map (fun pattern -> pattern, (input 
                                                        |> Seq.map (fun item -> (findMatches (item.Value |> List.ofSeq) pattern 0).Length <> 0)
                                                        |> Seq.forall (fun item -> item)))
                    |> Seq.filter (fun (sequence, found) -> found)

        if res |> Seq.isEmpty then
            findCommonSubstringsOfGivenSize input toFind (size - 1)
        else 
            res |> Seq.map (fun (item, found) -> item)

let findCommonSubstrings (input:Map<int,char list>) =
    let first = input.First().Value
    findCommonSubstringsOfGivenSize input first (first.Length)

let result = findCommonSubstrings read
                |> Seq.map (fun (str) -> sprintf "%s" (String.Join("", str)))
    


File.WriteAllText(output, sprintf "%s" (result.First()))
