import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreFooterComponent } from './ire-footer.component';

describe('IreFooterComponent', () => {
  let component: IreFooterComponent;
  let fixture: ComponentFixture<IreFooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreFooterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
