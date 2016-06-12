#load "StringHelpers.fsx"
#load "IO.fsx"

let exercise = "lcsm"

open System
open System.Linq
open StringHelpers
open IO

let read = IO.ReadInputLines exercise
            |> StringHelper.mergeFASTA
            |> Seq.chunkBySize 2
            |> Seq.map (fun item -> (int <| item.[0].Substring(item.[0].IndexOf("_") + 1)), item.[1] |> List.ofSeq)
            |> Map.ofSeq


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
                    |> Seq.map (fun pattern -> 
                        pattern, (input 
                                    |> Seq.map (fun item -> (StringHelper.findMatches (item.Value |> List.ofSeq) pattern 0).Length <> 0)
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
    

IO.WriteOutput exercise (result.First())
