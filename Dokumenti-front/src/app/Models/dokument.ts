import { StavkeDokumenta } from "./stavke-dokumenta";

export class Dokument {
    dokumentId: number;
    datum: Date;
    datumDospeca: Date;
    ukupnaCena: number;
    mestoDospeca: string;
    klijentId: number;
    tipDokumentaId: number;
    datumKreiranja: Date;
    stavkeDokumenta: StavkeDokumenta[];
}