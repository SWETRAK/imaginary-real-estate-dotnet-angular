import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreServerErrorPageComponent } from './ire-server-error-page.component';

describe('IreServerErrorPageComponent', () => {
  let component: IreServerErrorPageComponent;
  let fixture: ComponentFixture<IreServerErrorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreServerErrorPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreServerErrorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
