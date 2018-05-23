import { Component, OnInit, Inject } from '@angular/core';
import '../../js/ymaps.js'
import { ReserveManger } from '../../services/reserve-manager';
import { Car } from '../../models/car';
import { Remote } from '../../services/http-client';
import { HubConnection } from '@aspnet/signalr';
declare var ymaps:any
@Component({
  selector: 'app-car-map',
  templateUrl: './car-map.component.html',
  styleUrls: ['./car-map.component.css']
})
export class CarMapComponent implements OnInit {
  ymaps:any;
  private _hubConnection: HubConnection;
  private carCollection:any[]=[];
  constructor(@Inject(Remote)private remote:Remote,private reserveManager:ReserveManger) { }
  ngOnInit() {
    ymaps.ready().then(()=>{
      this.ymaps=new ymaps.Map("main_map",{
        center: [51.128433, 71.430546],
        zoom: 12,
        controls: []
      });
      this.ymaps.container.enterFullscreen();
      this.InitBaloons()
    })

  }
  private InitBaloons(){
    this._hubConnection = new HubConnection(this.remote.baseUri+"/vehicle");
    this._hubConnection.start()
        .then(() => {
            console.log('Hub connection started')
        })
        .catch(() => {
            console.log('Error while establishing connection')
        });
        this._hubConnection.on('onInitialize',(data:Car[])=>{
          this.OnInitialize(data);
      });
      this._hubConnection.on('onUnlocked',(data:Car)=>{
        this.OnUnlocked(data);
    });
    this._hubConnection.on('onReserved',(data:number)=>{
      this.OnReserved(data);
  });
  }
  OnInitialize(data:Car[]){
    data.forEach((v,i)=>{
      let myPlacemark = new ymaps.Placemark([v.x,v.y], {
          balloonContentBody: [
              '<div class="card" style="width: 18rem;"> <img class="card-img-top" style="width:100px;"src="https://l-userpic.livejournal.com/111628924/39184714" alt="Card image cap"> <div class="card-body">'+
            '<h5 class="card-title">'+v.brand+'</h5> <p class="card-text">Класс-'+v.class+'</p>'+
            '</div> <ul class="list-group list-group-flush"> <li class="list-group-item">Цена за киллометр - '+v.cost+' тг</li> <li class="list-group-item">Номер - '+v.number+'</li>'+
            '<li class="list-group-item">Vestibulum at eros</li> </ul> <div class="card-body"> <a href="'+this.remote.baseUri+'/ReserveCar?carId='+v.id+'" class="card-link">Забронировать</a>'+
              '<a href="#" class="card-link">Another link</a>'+
              '</div>'+
              '</div>'
          ].join('')
      }, {
          preset: 'islands#redDotIcon'
      });
      myPlacemark.name=v.id;
      this.carCollection.push(myPlacemark);
      this.ymaps.geoObjects.add(myPlacemark);
    })
  }
  OnUnlocked(data:Car){
    let myPlacemark = new ymaps.Placemark([data.x,data.y], {
      balloonContentBody: [
          '<div class="card" style="width: 18rem;"> <img class="card-img-top" style="width:100px;"src="https://l-userpic.livejournal.com/111628924/39184714" alt="Card image cap"> <div class="card-body">'+
        '<h5 class="card-title">'+data.brand+'</h5> <p class="card-text">Класс-'+data.class+'</p>'+
        '</div> <ul class="list-group list-group-flush"> <li class="list-group-item">Цена за киллометр - '+data.cost+' тг</li> <li class="list-group-item">Номер - '+data.number+'</li>'+
        '<li class="list-group-item">Vestibulum at eros</li> </ul> <div class="card-body"> <a href="'+this.remote.baseUri+'/ReserveCar?carId='+data.id+'" class="card-link">Забронировать</a>'+
          '<a href="#" class="card-link">Another link</a>'+
          '</div>'+
          '</div>'
      ].join('')
  },{
      preset: 'islands#redDotIcon'
  });
  myPlacemark.name=data.id;
  this.carCollection.push(myPlacemark);
  this.ymaps.geoObjects.add(myPlacemark);
  }
  OnReserved(id:number){
    debugger
    let car =this.carCollection.filter(item=>
      item.name===id
    )[0];
    this.ymaps.geoObjects.remove(car);
  }
}
