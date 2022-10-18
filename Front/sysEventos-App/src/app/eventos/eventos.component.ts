import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any =[];
  public eventosFilter: any =[];
  widthImg: number = 130;
  marginImg: number =2 ;
  showImg: boolean = true;
  private _filterList: string = '';

  public get filterList(): string{
    return this._filterList;
  }

  public set filterList(value: string){
    this._filterList = value;
    //condição
    this.eventosFilter = this.filterList ? this.filterEventos(this.filterList) : this.eventos
  }

  filterEventos(filterper:string):any{
    filterper = filterper.toLocaleUpperCase();
    return this.eventos.filter(
      (evento: { tema: string, local: string }) => evento.tema.toLocaleUpperCase().indexOf(filterper) !== -1 ||
      evento.local.toLocaleUpperCase().indexOf(filterper) !== -1



    );
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();

  }

  alterImg(){
    this.showImg = !this.showImg;
  }



  public getEventos():void{
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response;
        this.eventosFilter = this.eventos;
      },
      error => console.log(error)
    );

  }

}
