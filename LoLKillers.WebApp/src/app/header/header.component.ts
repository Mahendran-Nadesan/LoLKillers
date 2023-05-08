import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})


export class HeaderComponent implements OnInit {

  // move somewhere, or load from db?
  REGIONS: string[] = [
    'EUW',
    'NA',
    'OCE'
  ];
  
  selectedRegion = this.REGIONS[0];

  collapsed = true;
  
  

  ngOnInit(): void {
    
  }

  searchSummoner(){
    
  }

  printSelectedRegion() {
    console.log(this.selectedRegion);
  }

}
