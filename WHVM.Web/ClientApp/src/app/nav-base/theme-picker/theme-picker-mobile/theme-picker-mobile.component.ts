import { Component, OnInit } from '@angular/core';
import { ThemePickerComponent} from '../theme-picker.component';
import { ThemeService } from '../../../theme.service';

@Component({
  selector: 'app-theme-picker-mobile',
  templateUrl: './theme-picker-mobile.component.html',
  styleUrls: ['./theme-picker-mobile.component.scss']
})
export class ThemePickerMobileComponent extends ThemePickerComponent implements OnInit  {

  constructor(themeService: ThemeService) {
    super(themeService);
  }

  ngOnInit() {
  }

}
