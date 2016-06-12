﻿#load "StringHelpers.fsx"

let input = "/tmp/rosalind_gc.txt"
let output = "/tmp/rosalind_gcresult.txt"

open System.IO
open System
open System.Linq
open StringHelpers

let aggregate (state: Map<string, float>) (row:string) (header:string) =
    let sum = row 
                |> Seq.filter (fun item -> item = 'C' || item = 'G') 
                |> Seq.sumBy (fun _ -> 1)
    let occ = Convert.ToDouble(sum) / Convert.ToDouble(row.Length)
    state.Add(StringHelper.extractFASTAId header, occ)

let result = File.ReadLines(input) |> StringHelper.mergeFASTA

let headers = result
                |> Seq.indexed
                |> Seq.filter (fun (idx, item) -> idx % 2 = 0)
                |> Seq.map (fun (idx, item) -> item)

let data = result
                |> Seq.indexed
                |> Seq.filter (fun (idx, item) -> idx % 2 = 1)
                |> Seq.map (fun (idx, item) -> item)

let acc = headers
            |> Seq.fold2 aggregate Map.empty data
            |> Seq.maxBy (fun (pair) -> pair.Value)

File.WriteAllText(output, (sprintf "Rosalind_%s\n%f" acc.Key (acc.Value * 100.0)))