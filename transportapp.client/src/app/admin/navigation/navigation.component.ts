import { Component, output } from '@angular/core';

@Component({
  selector: 'app-navigation',
  standalone: false,
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss'
})
export class NavigationComponent {
  // public props
  windowWidth: number;
  NavMobCollapse = output();

  // constructor
  constructor() {
    this.windowWidth = window.innerWidth;
  }

  // public method
  navMobCollapse() {
    if (this.windowWidth < 992) {
      this.NavMobCollapse.emit();
    }
  }
}
