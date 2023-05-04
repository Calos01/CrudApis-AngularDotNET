import { AfterViewInit, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Mascota } from 'src/app/interfaces/mascota';
import { MascotaService } from 'src/app/services/mascota.service';

@Component({
  selector: 'app-agregar-editar-mascota',
  templateUrl: './agregar-editar-mascota.component.html',
  styleUrls: ['./agregar-editar-mascota.component.css']
})
export class AgregarEditarMascotaComponent implements AfterViewInit{
  loading:boolean=false;
  form:FormGroup
  id!:number;
  titulo:string="Agregar";

  constructor(private fb: FormBuilder, private _snackBar:MatSnackBar, private _serviceMascota:MascotaService, private router: Router, private _aroute:ActivatedRoute) {
    this.form=this.fb.group({
      nombre:['', Validators.required],
      raza:['', Validators.required],
      edad:['', Validators.required],
      peso:['', Validators.required],
      color:['', Validators.required]
    }) 
    this.id=Number(this._aroute.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    if(this.id!=0){
      this.titulo="Editar";
      this.obtenerDatos(this.id);
    }
  }

  RegistraryEditardatos(){
    const mascota:Mascota={
      nombre: this.form.value.nombre,
      edad: this.form.value.edad,
      raza: this.form.value.raza,
      color: this.form.value.color,
      peso: this.form.value.peso
  }
    if(this.id!=0){
      this.editarMascota(this.id, mascota);
    }else{
      this.agregarMascota(mascota);
    } 
  }
  editarMascota(id:number,mascota:Mascota){
    mascota.id=this.id;
    this._serviceMascota.updateMascota(id,mascota).subscribe(()=>{
      this.mostrarMensaje("actualizado")
      this.router.navigate(['/ListadoMascota']);
    })
  }

  obtenerDatos(id:number){
    return this._serviceMascota.getmascota(id).subscribe(data=>{
      this.form.setValue({//aqui se puede usar el set(que trae a todos) y el patch(que trae a algunos)
        nombre: data.nombre,
        edad: data.edad,
        raza:data.raza,
        color: data.color,
        peso: data.peso
      })
    })
  }

  agregarMascota(mascota:Mascota){
    this._serviceMascota.addMascota(mascota).subscribe(data=>{
      console.log(data);
      this.mostrarMensaje("agregado");
      this.router.navigate(['/ListadoMascota']);
    })
  }
  
  ngAfterViewInit(): void {
  }

  mostrarMensaje(texto:string){
    this.loading=false;
    this._snackBar.open(`Elemento ${texto} exitosamente`, '', {
      duration: 4000,
      horizontalPosition:'end',
      verticalPosition:'bottom'
    });
}
}
