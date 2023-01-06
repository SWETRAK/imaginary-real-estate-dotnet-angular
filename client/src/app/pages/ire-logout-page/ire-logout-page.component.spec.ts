import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreLogoutPageComponent } from './ire-logout-page.component';

describe('IreLogoutPageComponent', () => {
  let component: IreLogoutPageComponent;
  let fixture: ComponentFixture<IreLogoutPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreLogoutPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreLogoutPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
