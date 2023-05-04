import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Mascota } from 'src/app/interfaces/mascota';
import { MascotaService } from 'src/app/services/mascota.service';

@Component({
  selector: 'app-ver-mascota',
  templateUrl: './ver-mascota.component.html',
  styleUrls: ['./ver-mascota.component.css']
})
export class VerMascotaComponent implements OnInit, OnDestroy {

  private id!:number;
  mascota!: Mascota;
  mascota$!: Observable<Mascota>
  routeSub!: Subscription;
  loading:boolean=false;
  constructor(private _serviceMascota: MascotaService, private _aRoute: ActivatedRoute) {
    //el aRoute devuelve un string por eso lo transformamos
    //this.id = Number(this._aRoute.snapshot.paramMap.get('id')) //ESTE ID es el mismo que se pone en el app-routing.module
    // Otras formas de cambiar a int
    // const id = +this._aRoute.snapshot.paramMap.get('id')! //ESTE ID es el mismo que se pone en el app-routing.module
    // const id = parseInt(this._aRoute.snapshot.paramMap.get('id')!) //ESTE ID es el mismo que se pone en el app-routing.module
    // console.log(this.id);
  }
  //Como obtener el id sin tenerlo en el constructor ya q eso solo se ejecuta 1 sola vez(Cosa excepcional)
  ngOnInit(): void {
    this.routeSub=this._aRoute.params.subscribe(data=>{
      this.id = data['id']
      this.obtenerMascotaPorId();
    })
    //this.mascota$=this._serviceMascota.getmascota(this.id);
  }
  //Siempre cuando se usa suscribe se debe usar en el destroy el unsubscribe (excepto en las peticiones http como el metodo obtenermascota)
  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }
  obtenerMascotaPorId() {
    this.loading=true;
    this._serviceMascota.getmascota(this.id).subscribe(data => {
      this.mascota=data;
      this.loading=false  
    });
}
}
